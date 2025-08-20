namespace PresTrust.FloodMitigation.Application.Commands;

public class RequestForApplicationCorrectionCommandHandler : BaseHandler, IRequestHandler<RequestForApplicationCorrectionCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationFeedbackRepository repoFeedback;
    private readonly IEmailManager repoEmailManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>

    public RequestForApplicationCorrectionCommandHandler
       (
           IMapper mapper,
           IPresTrustUserContext userContext,
           IOptions<SystemParameterConfiguration> systemParamOptions,
           IApplicationRepository repoApplication,
           IApplicationFeedbackRepository repoFeedback,
           IEmailManager repoEmailManager
       ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoFeedback = repoFeedback;
        this.repoEmailManager = repoEmailManager;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(RequestForApplicationCorrectionCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        AuthorizationCheck(application);

        // update feedback status and send email to an applicant
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoFeedback.RequestForApplicationCorrectionAsync(application.Id);

            //Get Template and Send Email
            //await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.FEEDBACK_EMAIL.ToString(), application);

            scope.Complete();
        };

        return true;
    }

    /// <summary>
    /// Ensure that a user has the relevant authorizations to perform an action
    /// </summary>
    private void AuthorizationCheck(FloodApplicationEntity application)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.REQUEST_FOR_AN_APPLICATION_CORRECTION);
    }
}
