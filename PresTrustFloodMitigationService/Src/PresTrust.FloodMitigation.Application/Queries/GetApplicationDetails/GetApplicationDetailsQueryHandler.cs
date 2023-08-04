namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationDetailsQueryHandler : BaseHandler, IRequestHandler<GetApplicationDetailsQuery, GetApplicationDetailsQueryViewModel>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly ICoreRepository repoCore;
    private readonly IApplicationRepository repoApplication;
    private readonly ICommentRepository repoComment;
    private readonly IFeedbackRepository repoFeedback;

    public GetApplicationDetailsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        ICoreRepository repoCore,
        IApplicationRepository repoApplication,
        ICommentRepository repoComment,
        IFeedbackRepository repoFeedback
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoCore = repoCore;
        this.repoApplication = repoApplication;
        this.repoComment = repoComment;
        this.repoFeedback = repoFeedback;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<GetApplicationDetailsQueryViewModel> Handle(GetApplicationDetailsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var result = mapper.Map<FloodApplicationEntity, GetApplicationDetailsQueryViewModel>(application);

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
