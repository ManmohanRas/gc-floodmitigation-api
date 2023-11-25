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
    private readonly IBrokenRuleRepository repoBrokenRules;

    public SaveProjectAreaCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IApplicationOverviewRepository repoOverview,
        IBrokenRuleRepository repoBrokenRules

        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoOverview = repoOverview;
        this.repoBrokenRules = repoBrokenRules;
    }

    public async Task<SaveProjectAreaCommandViewModel> Handle(SaveProjectAreaCommand request, CancellationToken cancellationToken)
    {
        // check if application exists
        var application = await GetIfApplicationExists(request.Id);


        // update application
        application.ApplicationSubTypeId = request.ApplicationSubTypeId;
        application.LastUpdatedBy = userContext.Email;

        //get application parcels
        var reqAppParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);

        //update application parcels
        reqAppParcels = reqAppParcels.Where(o => request.Parcels.Contains(o.PamsPin)).Select(o => new FloodApplicationParcelEntity()
        {
            ApplicationId = application.Id,
            PamsPin = o.PamsPin,
            Status = o.Status,
            IsLocked = false
        }).ToList();

        // update overview
        var reqAppOverview = await repoOverview.GetOverviewDetailsAsync(application.Id);
        reqAppOverview.ApplicationId = application.Id;
        reqAppOverview.NoOfHomes = request.NoOfHomes;
        reqAppOverview.NoOfContiguousHomes = request.NoOfContiguousHomes;
        reqAppOverview.LastUpdatedBy = userContext.Email;
        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(application, reqAppOverview);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {   // Delete old Broken Rules, if any
            await repoBrokenRules.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.PROJECT_AREA);
            // Save current Broken Rules, if any
            await repoBrokenRules.SaveBrokenRules(brokenRules);

            await repoApplication.SaveAsync(application);
            await repoApplicationParcel.DeleteApplicationParcelsByApplicationIdAsync(application.Id);
            await repoApplicationParcel.SaveAsync(reqAppParcels);
            await repoOverview.SaveAsync(reqAppOverview);

            scope.Complete();
        }

        // get application details
        var result = mapper.Map<FloodApplicationEntity, SaveProjectAreaCommandViewModel>(application);

        // apply security
        FloodApplicationSecurityManager securityMgr = default;
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
                securityMgr = new FloodApplicationSecurityManager(userContext.Role, application.Status, application.PrevStatus, feedbacksReqForCorrections);
                break;
            default:
                securityMgr = new FloodApplicationSecurityManager(userContext.Role, application.Status, application.PrevStatus);
                break;
        }
        result.Permission = securityMgr.Permission;
        result.NavigationItems = securityMgr.NavigationItems;
        result.AdminNavigationItems = securityMgr.AdminNavigationItems;
        result.PostApprovedNavigationItems = securityMgr.PostApprovedNavigationItems;
        result.DefaultNavigationItem = securityMgr.DefaultNavigationItem;
        return result;
    }

    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application, FloodApplicationOverviewEntity reqAppOverview)
    {
        int sectionId = (int)ApplicationSectionEnum.PROJECT_AREA;
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        // add based on the empty check conditions
        //if (application.Status == ApplicationStatusEnum.SUBMITTED || application.Status == ApplicationStatusEnum.IN_REVIEW || application.Status == ApplicationStatusEnum.ACTIVE || application.Status == ApplicationStatusEnum.CLOSED)
        //{
        //    if (reqAppOverview.NoOfHomes == null)
        //    {
        //        brokenRules.Add(new FloodBrokenRuleEntity()
        //        {
        //            ApplicationId = application.Id,
        //            SectionId = sectionId,
        //            Message = "No. Of Homes required field on Project Area tab have not been filled.",
        //            IsApplicantFlow = true
        //        });
        //    }

        //    if (reqAppOverview.NoOfContiguousHomes == null)
        //    {
        //        brokenRules.Add(new FloodBrokenRuleEntity()
        //        {
        //            ApplicationId = application.Id,
        //            SectionId = sectionId,
        //            Message = "No. Of Contiguous Homes required field on Project Area tab have not been filled.",
        //            IsApplicantFlow = true
        //        });
        //    }
        //}
        return brokenRules;
    }
}
