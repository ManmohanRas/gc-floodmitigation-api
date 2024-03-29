namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyDetailsQueryHandler : BaseHandler, IRequestHandler<GetPropertyDetailsQuery, GetPropertyDetailsQueryViewModel>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly ICoreRepository repoCore;
    private readonly IParcelRepository repoParcel;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationCommentRepository repoComment;
    private readonly IApplicationFeedbackRepository repoFeedback;

    public GetPropertyDetailsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        ICoreRepository repoCore,
        IParcelRepository repoParcel,
        IApplicationRepository repoApplication,
        IApplicationCommentRepository repoComment,
        IApplicationFeedbackRepository repoFeedback
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoCore = repoCore;
        this.repoParcel = repoParcel;
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
    public async Task<GetPropertyDetailsQueryViewModel> Handle(GetPropertyDetailsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await repoParcel.GetParcelAsync(application.Id, request.PamsPin);
        var result = mapper.Map<FloodParcelEntity, GetPropertyDetailsQueryViewModel>(property);

        // apply security
        FloodPropertySecurityManager securityMgr = default;
        // derive user's role for a given agency
        userContext.DeriveRole(property.AgencyId);
        // derive navigation items & permissions
        switch (property.Status)
        {
            case PropertyStatusEnum.SUBMITTED:
            case PropertyStatusEnum.IN_REVIEW:
            case PropertyStatusEnum.PENDING:
                var feedbacksReqForCorrections = property.Feedbacks.Where(f => f.RequestForCorrection == true && string.Compare(f.CorrectionStatus, PropertyCorrectionStatusEnum.REQUEST_SENT.ToString(), true) == 0).ToList();
                securityMgr = new FloodPropertySecurityManager(userContext.Role, property.Status, property.PrevStatus, application.Status, feedbacksReqForCorrections, property.IsSubmitted ?? false);
                break;
            default:
                securityMgr = new FloodPropertySecurityManager(userContext.Role, property.Status, property.PrevStatus, application.Status, null, property.IsSubmitted ?? false);
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
