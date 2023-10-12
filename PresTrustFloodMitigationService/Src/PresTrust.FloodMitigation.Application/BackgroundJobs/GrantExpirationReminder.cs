using MediatR;
using OneOf.Types;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.BackgroundJobs;

public class GrantExpirationReminder : BaseHandler, IGrantExpirationReminder
{
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IEmailTemplateRepository repoEmailTemplate;
    private readonly IReminderEmailManager repoEmailManager;

    public GrantExpirationReminder
    (
           IOptions<SystemParameterConfiguration> systemParamOptions,
           IApplicationRepository repoApplication,
           IEmailTemplateRepository repoEmailTemplate,
           IReminderEmailManager repoEmailManager
    ) : base(repoApplication)
    {
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoEmailTemplate = repoEmailTemplate;
        this.repoEmailManager = repoEmailManager;
    }

    public void SendEmail(string backGroundJobType, string startTime)
    {
        Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
    }

    public async Task Handle()
    {
        // Get list of applications with properties that will expire in 3 months on Grant Expiration Date. 

        // Iterate through each application and fetch properties that will expire in 3 months on Grant Expiration Date.

        // Get Email template

        // Send Email

        Console.WriteLine("Handle - " + DateTime.Now.ToLongTimeString());

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            var template = await repoEmailTemplate.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_DOI_DRAFT_TO_DOI_SUBMITTED.ToString());
            if (template != null)
            {
                await repoEmailManager.SendMail(subject: template.Subject ?? "", htmlBody: template.Description ?? "", applicationId: 6, applicationName: "Test Application", propertyName:"Test Property");
            }
            scope.Complete();
        }

        await Task.Yield();
    }
}

 