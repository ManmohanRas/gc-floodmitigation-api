namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class RequestForPropertyCorrectionCommandHandler : BaseHandler, IRequestHandler<RequestForPropertyCorrectionCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackPropRepository repoFeedback;
    private readonly IEmailManager repoEmailManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    /// 
    public RequestForPropertyCorrectionCommandHandler
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
    public async Task<bool> Handle(RequestForPropertyCorrectionCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var application = await GetIfApplicationExists(request.ApplicationId);
        //AuthorizationCheck(application);
        // update feedback status and send email to an applicant
        //  using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        //{
          await repoFeedback.RequestForPropertyCorrectionAsync(application.Id, request.PamsPin);

           // var template = await repoEmailTemplate.GetEmailTemplate(EmailTemplateCodeTypeEnum.FEEDBACK_EMAIL.ToString());
           // if (template != null)
             //   await repoEmailManager.SendMail(subject: template.Subject, applicationId: application.Id, applicationName: application.Title, htmlBody: template.Description, fundingYear: application.FundingYear, agencyId: application.AgencyId);

           // scope.Complete();
        //};

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
