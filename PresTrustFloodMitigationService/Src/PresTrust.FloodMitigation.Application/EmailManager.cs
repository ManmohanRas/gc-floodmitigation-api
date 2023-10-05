namespace PresTrust.FloodMitigation.Application;

public interface IEmailManager
{
    Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, int agencyId = default);
}
public class EmailManager : IEmailManager
{
    private readonly IMapper mapper;
    private readonly IEmailApiConnect emailApiConnect;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPresTrustUserContext userContext;
    private readonly IIdentityApiConnect identityApiConnect;

    public EmailManager(
              IMapper mapper,
              IEmailApiConnect emailApiConnect,
              IOptions<SystemParameterConfiguration> systemParamOptions,
              IPresTrustUserContext userContext,
              IIdentityApiConnect identityApiConnect
        )
    {
        this.mapper = mapper;
        this.emailApiConnect = emailApiConnect;
        this.systemParamOptions = systemParamOptions.Value;
        this.userContext = userContext;
        this.identityApiConnect = identityApiConnect;
    }

    private Task<Tuple<List<string>,List<string>>> GetPrimaryContact(int applicationId, int agencyId)
    {            
        List<string> primaryContactNames = new List<string>() {"MG", "Manmohan", "Sai Charan" };
        List<string> primaryContactEmails = new List<string>() {"mgthirumalesh@rightanglesol.com", "manmohan@rightanglesol.com", "saicharan@rightanglesol.com"};
        return Task.FromResult(new Tuple<List<string>, List<string>>(primaryContactNames, primaryContactEmails));
    }

    public async Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, int agencyId = default)
    {
        var primaryContact = await GetPrimaryContact(applicationId, agencyId);
       
        htmlBody = htmlBody.Replace("{{ProgramAdmin}}",userContext.IsExternalUser ? systemParamOptions.ProgramAdminName : userContext.Name ?? "");
        htmlBody = htmlBody.Replace("{{ProgramAdminEmail}}", userContext.Email ?? "");
        htmlBody = htmlBody.Replace("{{AgencyAdmin}}", userContext.Name ?? "");
        htmlBody = htmlBody.Replace("{{Applicant}}", userContext.Name ?? "");
        htmlBody = htmlBody.Replace("{{ProgramAdminPhoneNumber}}", "");
        htmlBody = htmlBody.Replace("{{PrimaryContactName}}", string.Join(",", primaryContact.Item1));
        htmlBody = htmlBody.Replace("{{ApplicationId}}", applicationId.ToString() ?? "");
        htmlBody = htmlBody.Replace("{{ApplicationName}}", applicationName ?? "");
        htmlBody = htmlBody.Replace("{{ProjectName}}", applicationName ?? "");

        subject = subject.Replace("{{ApplicationName}}", applicationName ?? "");

        var toEmails = systemParamOptions.IsDevelopment == false ?  string.Join(",", primaryContact.Item2) : systemParamOptions.TestEmailIds;

        var senderName = systemParamOptions.IsDevelopment == false ? userContext.Name : systemParamOptions.TestEmailFromUserName;
        var senderEmail = systemParamOptions.IsDevelopment == false ? userContext.Email : "mcgis@co.morris.nj.us";

        await this.Send(subject: subject, toEmails: toEmails, senderName: senderName, senderEmail: senderEmail, htmlBody: htmlBody);
    }

    private async Task Send(string subject, string toEmails, string senderName, string senderEmail, string htmlBody, string cc = null, string bcc = null)
    {
        var retry = Policy
            .Handle<Exception>()
            .WaitAndRetry(systemParamOptions.PollyRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(systemParamOptions.PollyRetryIntervalInSeconds, retryAttempt)));

        var postUserJson = new JsonContent(new EmailRequest()
        {
            Subject = subject,
            To = toEmails,
            SenderName = senderName,
            SenderEmail = senderEmail,
            CC = cc,
            BCC = bcc,
            HtmlBody = htmlBody,
            Attachments = null
        });

        try
        {
            await retry.Execute(async () =>
            {
                // call external api - EmailApi
                var result = await this.emailApiConnect.PostDataAsync<EmailResponse, JsonContent>($"{systemParamOptions.EmailApiSubDomain}/Email", postUserJson);
            });
        }
        catch
        {
            throw new Exception("Error occured while sending the email.");
        }
    }
}
