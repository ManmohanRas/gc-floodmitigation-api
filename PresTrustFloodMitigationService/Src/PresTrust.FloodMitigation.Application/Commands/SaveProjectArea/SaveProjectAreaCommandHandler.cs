using Microsoft.AspNetCore.Builder;

namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class SaveProjectAreaCommandHandler : BaseHandler, IRequestHandler<SaveProjectAreaCommand, SaveProjectAreaCommandViewModel>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IApplicationOverviewRepository repoOverview;

    public SaveProjectAreaCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IApplicationOverviewRepository repoOverview
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoOverview = repoOverview;
    }

    public async Task<SaveProjectAreaCommandViewModel> Handle(SaveProjectAreaCommand request, CancellationToken cancellationToken)
    {
        // check if application exists
        var application = await GetIfApplicationExists(request.Id);
        
        // update application
        application.ApplicationSubTypeId = request.ApplicationSubTypeId;
        application.LastUpdatedBy = userContext.Email;

        //update application parcels
        var reqAppParcels = request.Parcels.Select(o => new FloodApplicationParcelEntity()
        {
            ApplicationId = application.Id,
            PamsPin = o.PamsPin,
            IsLocked = false
        }).ToList();

        // update overview
        var reqAppOverview = await repoOverview.GetOverviewDetailsAsync(application.Id) ?? new FloodApplicationOverviewEntity()
        {
            ApplicationId = application.Id
        };
        reqAppOverview.NoOfHomes = request.NoOfHomes;
        reqAppOverview.NoOfContiguousHomes = request.NoOfContiguousHomes;
        reqAppOverview.LastUpdatedBy = userContext.Email;

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplication.SaveAsync(application);
            await repoApplicationParcel.DeleteApplicationParcelsByApplicationIdAsync(application.Id);
            await repoApplicationParcel.SaveAsync(reqAppParcels);
            await repoOverview.SaveAsync(reqAppOverview);

            scope.Complete();
        }

        // get application details
        var result = mapper.Map<FloodApplicationEntity, SaveProjectAreaCommandViewModel>(application);

        // apply security
        FloodSecurityManager securityMgr = default;
        // derive user's role for a given agency
        userContext.DeriveRole(application.AgencyId);
        // derive navigation items & permissions
        switch (application.Status)
        {
            case ApplicationStatusEnum.DOI_SUBMITTED:
            case ApplicationStatusEnum.SUBMITTED:
            case ApplicationStatusEnum.IN_REVIEW:
            case ApplicationStatusEnum.ACTIVE:
                var feedbacksReqForCorrections = application.Feedbacks.Where(f => f.RequestForCorrection == true && string.Compare(f.CorrectionStatus, ApplicationCorrectionStatusEnum.REQUEST_SENT.ToString(), true) == 0).ToList();
                securityMgr = new FloodSecurityManager(userContext.Role, application.Status, feedbacksReqForCorrections);
                break;
            default:
                securityMgr = new FloodSecurityManager(userContext.Role, application.Status);
                break;
        }
        result.Permission = securityMgr.Permission;
        result.NavigationItems = securityMgr.NavigationItems;
        result.AdminNavigationItems = securityMgr.AdminNavigationItems;
        result.PostApprovedNavigationItems = securityMgr.PostApprovedNavigationItems;
        result.DefaultNavigationItem = securityMgr.DefaultNavigationItem;
        return result;
    }
}
