namespace PresTrust.FloodMitigation.Application.BackgroundJobs;

public class ProjectAreaExpirationReminder : BaseHandler, IProjectAreaExpirationReminder
{
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IEmailTemplateRepository repoEmailTemplate;
    private readonly IReminderEmailManager repoEmailManager;

    public ProjectAreaExpirationReminder
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

    public async Task Handle()
    {
        Console.WriteLine("Handle - " + DateTime.Now.ToLongTimeString());
        var reminderApplications = await repoEmailTemplate.RemindingAboutProjectAreaExpiration();

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            var template = await repoEmailTemplate.GetEmailTemplate(EmailTemplateCodeTypeEnum.PROJECT_AREA_EXPIRATION_REMINDER.ToString());
            if (template != null)
            {
                foreach (var application in reminderApplications)
                {
                    await repoEmailManager.SendMail(subject: template.Subject ?? "", htmlBody: template.Description ?? "", applicationId: application.Id, applicationName: application.Title, propertyName: default, agencyId: application.AgencyId, expirationDate: application.ExpirationDate);
                }
            }
            scope.Complete();
        }

        await Task.Yield();
    }
}
