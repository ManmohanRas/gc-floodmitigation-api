namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class CreateApplicationCommandHandler : BaseHandler, IRequestHandler<CreateApplicationCommand, CreateApplicationCommandViewModel>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;

    public CreateApplicationCommandHandler(
        IMapper  mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
    }

    public async Task<CreateApplicationCommandViewModel> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        //check permissions
        AuthorizationCheck(request);

        // create application
        var reqApplication = mapper.Map<CreateApplicationCommand, FloodApplicationEntity>(request);
        reqApplication.Status = ApplicationStatusEnum.DOI_DRAFT;
        reqApplication.CreatedByProgramAdmin = userContext.Role == UserRoleEnum.PROGRAM_ADMIN;
        reqApplication.LastUpdatedBy = userContext.Email;

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            reqApplication = await repoApplication.SaveAsync(reqApplication);

            FloodApplicationStatusLogEntity appStatusLog = new()
            {
                ApplicationId = reqApplication.Id,
                StatusId = reqApplication.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = reqApplication.LastUpdatedBy
            };
            await repoApplication.SaveStatusLogAsync(appStatusLog);

            scope.Complete();
        }

        // get application details
        var application = await GetIfApplicationExists(reqApplication.Id);
        var result = mapper.Map<FloodApplicationEntity, CreateApplicationCommandViewModel>(application);

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

    private void AuthorizationCheck(CreateApplicationCommand request)
    {
        userContext.DeriveRole(request.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, applicationStatus: ApplicationStatusEnum.NONE, operation: UserPermissionEnum.CREATE_APPLICATION);
    }
}
