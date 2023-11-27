using System;

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

        //var toEmails = systemParamOptions.IsDevelopment == false ?  string.Join(",", primaryContact.Item2) : systemParamOptions.TestEmailIds;
        var toEmails =  systemParamOptions.TestEmailIds;

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

public interface IReminderEmailManager
{
    Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, string propertyName);
}

public class ReminderEmailManager : IReminderEmailManager
{
    private readonly IReminderEmailApiConnect emailApiConnect;
    private readonly IApplicationUserRepository repoApplicationRole;
    private readonly SystemParameterConfiguration systemParamOptions;

    public ReminderEmailManager(
              IReminderEmailApiConnect emailApiConnect,
              IApplicationUserRepository repoApplicationRole,
              IOptions<SystemParameterConfiguration> systemParamOptions
        )
    {
        this.emailApiConnect = emailApiConnect;
        this.repoApplicationRole = repoApplicationRole;
        this.systemParamOptions = systemParamOptions.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="htmlBody"></param>
    /// <param name="applicationName"></param>
    /// <returns></returns>
    public async Task SendMail(string subject, string htmlBody, int applicationId, string applicationName = "", string propertyName = "")
    {
        List<string> primaryContactEmails;

        // get primary contacts
        var primaryContacts = await repoApplicationRole.GetPrimaryContactsAsync(applicationId);
        primaryContactEmails = primaryContacts.Select(i => i.Email).ToList();

        htmlBody = htmlBody.Replace("{{ProgramAdmin}}", systemParamOptions.ProgramAdminName);

        subject = subject.Replace("{{ApplicationName}}", applicationName ?? "");
        subject = subject.Replace("{{PropertyName}}", propertyName ?? "");

        var toEmails = systemParamOptions.IsDevelopment == false ? string.Join(",", primaryContactEmails) : systemParamOptions.TestEmailIds;
        var senderName = systemParamOptions.ProgramAdminName;
        var senderEmail = "mcgis@co.morris.nj.us";

        await this.Send(subject: subject, toEmails: toEmails, senderName: senderName, senderEmail: senderEmail, htmlBody: htmlBody);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="toEmails"></param>
    /// <param name="senderName"></param>
    /// <param name="senderEmail"></param>
    /// <param name="htmlBody"></param>
    /// <param name="cc"></param>
    /// <param name="bcc"></param>
    /// <returns></returns>
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
                var result = await this.emailApiConnect.PostDataAsync<EmailResponse, JsonContent>($"{systemParamOptions.EmailApiSubDomain}/Reminder", postUserJson);
            });
        }
        catch (Exception ex)
        {
            var x = ex.Message;
            throw new Exception("Error occured while sending the email.");
        }
    }
}

// https://stackoverflow.com/questions/69926026/authenticating-net-console-applications-with-net-core-web-api

//https://stackoverflow.com/questions/63699424/identityserver4-using-apikey-or-basic-authentication-directly-to-api

//https://github.com/mihirdilip/aspnetcore-authentication-apikey

//https://www.youtube.com/watch?v=eGj72-LqqAg

//https://www.youtube.com/watch?v=GrJJXixjR8M