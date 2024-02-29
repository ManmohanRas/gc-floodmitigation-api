using MediatR;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application;

public interface IEmailManager
{
    Task GetEmailTemplate(string emailTemplateCode, FloodApplicationEntity application);
    Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, int agencyId = default);
}

public class EmailManager : IEmailManager
{
    private readonly IMapper mapper;
    private readonly IEmailApiConnect emailApiConnect;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPresTrustUserContext userContext;
    private readonly IIdentityApiConnect identityApiConnect;
    private readonly IApplicationUserRepository repoApplicationUser;
    private IEmailTemplateRepository repoEmailTemplate;
    private IContactRepository repoContact;


    public EmailManager(
              IMapper mapper,
              IEmailApiConnect emailApiConnect,
              IOptions<SystemParameterConfiguration> systemParamOptions,
              IPresTrustUserContext userContext,
              IIdentityApiConnect identityApiConnect,
              IApplicationUserRepository repoApplicationUser,
              IEmailTemplateRepository repoEmailTemplate,
              IContactRepository repoContact
        )
    {
        this.mapper = mapper;
        this.emailApiConnect = emailApiConnect;
        this.systemParamOptions = systemParamOptions.Value;
        this.userContext = userContext;
        this.identityApiConnect = identityApiConnect;
        this.repoApplicationUser = repoApplicationUser;
        this.repoEmailTemplate = repoEmailTemplate;
        this.repoContact = repoContact;
    }

    private async Task<Tuple<List<string>,List<string>, List<string>>> GetPrimaryContact(int applicationId, int agencyId)
    {
        List<string> primaryContactNames = new List<string>();
        List<string> primaryContactEmails = new List<string>();
        List<string> alternateContactEmails = new List<string>();

        var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood/{agencyId}";
        var agencyUsers = await identityApiConnect.GetDataAsync<List<IdentityApiUser>>(endPoint);
        var primaryContacts = await repoApplicationUser.GetPrimaryContactsAsync(applicationId);
        var applicationUsers = await repoApplicationUser.GetApplicationUsersAsync(applicationId);


        var primaryAgencyUsers = agencyUsers.Where(o => primaryContacts.Select(x => x.Email?.Trim()).Contains(o.Email?.Trim()));
        primaryContactEmails = primaryContacts.Select(i => i.Email).ToList();
        alternateContactEmails = applicationUsers.Where(x => x.IsAlternateContact).Select(o => o.Email).ToList();

        if (primaryAgencyUsers.Count() > 0)
        {
            primaryContactNames = primaryAgencyUsers.Select(o => string.Format("{0} {1}", o.FirstName, o.LastName)).ToList();
        }
        else
        {
            primaryContactNames = primaryContacts.Select(o => string.Format("{0} {1}", o.FirstName, o.LastName)).ToList();
        }

        return new Tuple<List<string>, List<string>, List<string>>(primaryContactNames, primaryContactEmails, alternateContactEmails);
    }

    public async Task GetEmailTemplate(string emailTemplateCode, FloodApplicationEntity application)
    {
        var template = await repoEmailTemplate.GetEmailTemplate(emailTemplateCode);
        if (template != null)
        {
            await SendMail(subject: template.Subject, applicationId: application.Id, applicationName: application.Title, htmlBody: template.Description, agencyId: application.AgencyId);
        }
    }

    public async Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, int agencyId = default)
    {
        List<string> primaryContactNames = new List<string>();
        List<string> primaryContactEmails = new List<string>();
        List<string> alternateContactEmails = new List<string>();
        List<string> contactEmails = new List<string>();

        var primaryContact = await GetPrimaryContact(applicationId, agencyId);
        //agency users by
        //var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood/{agencyId}";
        //var agencyUsers = await identityApiConnect.GetDataAsync<List<IdentityApiUser>>(endPoint);

        //var primaryContacts = await repoApplicationUser.GetApplicationUsersAsync(applicationId);

        var contacts = await repoContact.GetAllContactsAsync(applicationId);

        //alternateContactEmails = primaryContacts.Where(o => o.IsAlternateContact).Select(o => o.Email).ToList();
        //primaryContactEmails = primaryContacts.Where(o => o.IsPrimaryContact).Select(o => o.Email).ToList();
        ////primaryContactNames = primaryContacts.Where(o => o.IsPrimaryContact).Select(o => string.Concat(agencyUsers.Select(o => o.FirstName + ' ' + o.LastName))).ToList();
        //primaryContactNames = primaryContacts.Where(x => x.IsPrimaryContact).Select(o => o.UserName).ToList();
        contactEmails = contacts.Where(o => o.SelectContact).Select(x => x.Email).ToList();

        htmlBody = htmlBody.Replace("{{ProgramAdmin}}",systemParamOptions.ProgramAdminName);
        htmlBody = htmlBody.Replace("{{ProgramAdminEmail}}", systemParamOptions.ProgramAdminEmail ?? "");
        htmlBody = htmlBody.Replace("{{AgencyAdmin}}", userContext.Name ?? "");
        htmlBody = htmlBody.Replace("{{Applicant}}", userContext.Name ?? "");
        htmlBody = htmlBody.Replace("{{ProgramAdminPhoneNumber}}", "");
        htmlBody = htmlBody.Replace("{{PrimaryContactName}}", string.Join(",", primaryContact.Item1));
        htmlBody = htmlBody.Replace("{{ApplicationId}}", applicationId.ToString() ?? "");
        htmlBody = htmlBody.Replace("{{ApplicationName}}", applicationName ?? "");
        htmlBody = htmlBody.Replace("{{ProjectName}}", applicationName ?? "");

        //new placeholders
        htmlBody = htmlBody.Replace("{{TotalEncumbered}}", "");
        htmlBody = htmlBody.Replace("{{CurrentExpirationDate}}", "");
        htmlBody = htmlBody.Replace("{{ContactName}}", "");
        htmlBody = htmlBody.Replace("{{FMPSoftCostReimbursed}}", "");
        htmlBody = htmlBody.Replace("{{FirstProjectAreaExtensionDate}}", "");
        htmlBody = htmlBody.Replace("{{SecondProjectAreaExtensionDate}}", "");

        //property
        htmlBody = htmlBody.Replace("{{PropertyName}}", "");
        htmlBody = htmlBody.Replace("{{MCHardCostShare}}", "");
        htmlBody = htmlBody.Replace("{{BCCDate}}", "");
        //property
        //new placeholders

        subject = subject.Replace("{{ApplicationName}}", applicationName ?? "");

        //var toEmails = systemParamOptions.IsDevelopment == false ?  string.Join(",", primaryContact.Item2) : systemParamOptions.TestEmailIds;

        var toEmails = string.Join(",", primaryContact.Item2);

        //appending Program admin to cc list 
        //string cc = string.Join(",", primaryContact.Item3);
        alternateContactEmails.Add(string.Join("", primaryContact.Item3));
        alternateContactEmails.Add(string.Join("", systemParamOptions.ProgramAdminEmail));
        //cc = string.Join(",", systemParamOptions.ProgramAdminEmail);


        //if contacts selectes then contact emails to cc list
        if (contactEmails.Count() > 0)
        {
            //cc = string.Join(",", contactEmails);
            alternateContactEmails.Add(string.Join("", contactEmails));
           
        }

        var cc = string.Join(",", alternateContactEmails);

        var senderName = systemParamOptions.IsDevelopment == false ? userContext.Name : systemParamOptions.TestEmailFromUserName;
        var senderEmail = systemParamOptions.IsDevelopment == false ? userContext.Email : "mcgis@co.morris.nj.us";

        await this.Send(subject: subject, toEmails: toEmails, senderName: senderName, senderEmail: senderEmail, htmlBody: htmlBody, cc: cc);
    }

    private async Task Send(string subject, string toEmails, string senderName, string senderEmail, string htmlBody, string cc, string bcc = null)
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
        catch(Exception ex)
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
