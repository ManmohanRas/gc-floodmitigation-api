namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class ResponseToRequestForApplicationCorrectionCommandHandler : BaseHandler, IRequestHandler<ResponseToRequestForApplicationCorrectionCommand, bool>
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
    public ResponseToRequestForApplicationCorrectionCommandHandler
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
    public async Task<bool> Handle(ResponseToRequestForApplicationCorrectionCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        AuthorizationCheck(application);

        // get feedback where the status is "Request Sent"
        var corrections = await repoFeedback.GetFeedbacksAsync(application.Id, correctionStatus: ApplicationCorrectionStatusEnum.REQUEST_SENT.ToString());
        // update feedback status as response received and send email to an applicant
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            foreach (var section in request.Sections)
            {
                Enum.TryParse(value: section, ignoreCase: true, out ApplicationSectionEnum enumSection);
                await repoFeedback.ResponseToRequestForApplicationCorrectionAsync(application.Id, (int)enumSection);
            }

            // If reponse's description/feedback is not empty
            if (!string.IsNullOrEmpty(request.Feedback))
            {
                var feedback = new FloodApplicationFeedbackEntity()
                {
                    Id = 0,
                    ApplicationId = application.Id,
                    CorrectionStatus = ApplicationCorrectionStatusEnum.NONE.ToString(),
                    Feedback = request.Feedback,
                    Section = ApplicationSectionEnum.NONE,
                    RequestForCorrection = false,
                    LastUpdatedBy = userContext.Name
                };

                await repoFeedback.SaveAsync(feedback);
            }

            //Get Template and Send Email
            await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.FEEDBACK_RESPONSE_EMAIL.ToString(), application);

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
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.RESPOND_TO_THE_REQUEST_FOR_AN_APPLICATION_CORRECTION);
    }
}
