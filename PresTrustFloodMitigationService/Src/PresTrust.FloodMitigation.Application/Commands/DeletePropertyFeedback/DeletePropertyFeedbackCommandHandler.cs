namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropertyFeedbackCommandHandler : BaseHandler, IRequestHandler<DeletePropertyFeedbackCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackPropRepository repoFeedback;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public DeletePropertyFeedbackCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IFeedbackPropRepository repoFeedback
    ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoFeedback = repoFeedback;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeletePropertyFeedbackCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        //AuthorizationCheck(application);

        //do authcheck for property also -- after property profile is complete

        // map command object to the FloodApplicationFeedbackEntity
        var reqFeedback = mapper.Map<DeletePropertyFeedbackCommand, FloodPropertyFeedbackEntity>(request);

        // delete feedback
        await repoFeedback.DeletePropertyFeedbackAsync(reqFeedback);
        return true;
    }

    /// <summary>
    /// Ensure that a user has the relevant authorizations to perform an action
    /// </summary>
    private void AuthorizationCheck(FloodApplicationEntity application)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.DELETE_FEEDBACKS);
    }
}

