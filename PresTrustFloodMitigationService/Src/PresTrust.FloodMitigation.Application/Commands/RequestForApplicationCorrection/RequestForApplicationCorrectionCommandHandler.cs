namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class RequestForApplicationCorrectionCommandHandler : IRequestHandler<RequestForApplicationCorrectionCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackRepository repoFeedback;
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
    public RequestForApplicationCorrectionCommandHandler
       (
           IMapper mapper,
           IPresTrustUserContext userContext,
           IOptions<SystemParameterConfiguration> systemParamOptions,
           IApplicationRepository repoApplication,
           IFeedbackRepository repoFeedback
       )
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
    public async Task<bool> Handle(RequestForApplicationCorrectionCommand request, CancellationToken cancellationToken)
    {

        // update feedback status and send email to an applicant
      //  using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
          //  await repoFeedback.RequestForApplicationCorrectionAsync(application.Id);

           // var template = await repoEmailTemplate.GetEmailTemplate(EmailTemplateCodeTypeEnum.FEEDBACK_EMAIL.ToString());
           // if (template != null)
             //   await repoEmailManager.SendMail(subject: template.Subject, applicationId: application.Id, applicationName: application.Title, htmlBody: template.Description, fundingYear: application.FundingYear, agencyId: application.AgencyId);

           // scope.Complete();
        };

        return true;
    }
}