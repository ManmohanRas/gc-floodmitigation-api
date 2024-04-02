namespace PresTrust.FloodMitigation.Application.BackgroundJobs;
public class PropertyDemolitionReminder : BaseHandler, IPropertyDemolitionReminder
{
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IEmailTemplateRepository repoEmailTemplate;
    private readonly IReminderEmailManager repoEmailManager;

    public PropertyDemolitionReminder
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
        var reminderProperties = await repoEmailTemplate.ReminderAboutPropertyExpiration();

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            var template = await repoEmailTemplate.GetEmailTemplate(EmailTemplateCodeTypeEnum.DEMOLITION_REMINDER.ToString());
            if (template != null)
            {
                foreach (var property in reminderProperties)
                {
                    await repoEmailManager.SendMail(subject: template.Subject ?? "", htmlBody: template.Description ?? "", applicationId: property.ApplicationId, applicationName: property.Title, propertyName: property.PropertyAddress, emailPlaceholders: property);
                }
            }
            scope.Complete();
        }

        await Task.Yield();
    }
}
