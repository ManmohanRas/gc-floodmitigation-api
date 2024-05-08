using System;

namespace PresTrust.FloodMitigation.Application;

public interface IEmailManager
{
    Task GetEmailTemplate(string emailTemplateCode, FloodApplicationEntity application, FloodApplicationParcelEntity? property = default, DateTime? emailDate = default);
    //Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, int agencyId = default, FloodApplicationParcelEntity? property = default, DateTime? emailDate = default);
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
    private readonly IApplicationReleaseOfFundsRepository repoApplicationROF;


    public EmailManager(
              IMapper mapper,
              IEmailApiConnect emailApiConnect,
              IOptions<SystemParameterConfiguration> systemParamOptions,
              IPresTrustUserContext userContext,
              IIdentityApiConnect identityApiConnect,
              IApplicationUserRepository repoApplicationUser,
              IEmailTemplateRepository repoEmailTemplate,
              IContactRepository repoContact,
              IApplicationReleaseOfFundsRepository repoApplicationROF
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
        this.repoApplicationROF = repoApplicationROF;
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

    public async Task GetEmailTemplate(string emailTemplateCode, FloodApplicationEntity application, FloodApplicationParcelEntity? property, DateTime? emailDate)
    {
        FloodEmailTemplatePlaceholdersEntity placeHolders = new FloodEmailTemplatePlaceholdersEntity();
        var template = await repoEmailTemplate.GetEmailTemplate(emailTemplateCode);
        decimal CAFAmount = 0;
        property = property ?? new FloodApplicationParcelEntity() { ApplicationId = application.Id };


        placeHolders = await repoEmailTemplate.EmailTemplatePlaceholders(application.Id, property.PamsPin);


        if (emailTemplateCode == EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_IN_REVIEW_TO_ACTIVE.ToString())
        {
            var payments = await repoApplicationROF.GetApplicationPaymentsAsync(application.Id);
            if (payments.Count() > 0)
            {
                decimal houseEncubrance = payments.Sum(y => y.EstimatePurchasePrice * y.MatchPercent / 100) ?? 0;
                var softEstimateInit = houseEncubrance * 25 / 100;
                var additionalSoftCostEstimate = payments.Sum(y => y.AdditionalSoftCostEstimate);
                decimal softEstimate = softEstimateInit + additionalSoftCostEstimate ?? 0;
                placeHolders.CAFAmount = houseEncubrance + softEstimate;
            }
        }

        if (template != null)
        {
            await SendMail(subject: template.Subject, applicationId: application.Id, applicationName: application.Title, htmlBody: template.Description, agencyId: application.AgencyId, emailDate: emailDate, placeHolders: placeHolders, emailTemplateCode: emailTemplateCode);
        }
    }

    public async Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, int agencyId = default, DateTime? emailDate = default, FloodEmailTemplatePlaceholdersEntity placeHolders = default, string emailTemplateCode = default)
    {
        List<string> primaryContactNames = new List<string>();
        List<string> primaryContactEmails = new List<string>();
        List<string> alternateContactEmails = new List<string>();
        List<string> contactEmails = new List<string>();
        string toEmails = string.Empty;
        DateTime? CurrentExpirationDate = placeHolders.SecondFundingExpirationDate ?? placeHolders.FirstFundingExpirationDate ?? placeHolders.FundingExpirationDate;

        var primaryContact = await GetPrimaryContact(applicationId, agencyId);

        var contacts = await repoContact.GetAllContactsAsync(applicationId);
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

        subject = subject.Replace("{{ApplicationName}}", applicationName ?? "");

        //new placeholders
        htmlBody = htmlBody.Replace("{{TotalEncumbered}}", placeHolders.CAFAmount?.ToString("C2", new CultureInfo("en-US")) ?? ""); //Total encumbered amount for project area – Take this from Project Area > Release of Funds(CAF)
        htmlBody = htmlBody.Replace("{{CurrentExpirationDate}}", CurrentExpirationDate?.ToString("dddd, MMMM dd, yyyy")); // Current expiration date, whether it is original, first extension, or second extension
        htmlBody = htmlBody.Replace("{{ContactName}}", string.Join(",", primaryContact.Item1));
        htmlBody = htmlBody.Replace("{{FMPSoftCostReimbursed}}", placeHolders.SoftCostFMPAmt.ToString());
        htmlBody = htmlBody.Replace("{{FirstProjectAreaExtensionDate}}", emailDate?.ToString("dddd, MMMM dd, yyyy"));
        htmlBody = htmlBody.Replace("{{SecondProjectAreaExtensionDate}}", emailDate?.ToString("dddd, MMMM dd, yyyy"));

        //property
        htmlBody = htmlBody.Replace("{{PropertyName}}", placeHolders.PropertyAddress);
        htmlBody = htmlBody.Replace("{{MCHardCostShare}}", placeHolders.HardCostFMPAmt.ToString()); //Hard Cost FMP Amount Reimbursed value from Property Finance screen
        htmlBody = htmlBody.Replace("{{BCCDate}}", placeHolders.BccFinalApprovalDate?.ToString("dddd, MMMM dd, yyyy"));

        //Batch job
        htmlBody = htmlBody.Replace("{{ProjectAreaExpirationDate}}", placeHolders.CurrentExpirationDate?.ToString("dddd, MMMM dd, yyyy")); //Expiration Date from Project Area Admin details tab
        htmlBody = htmlBody.Replace("{{GrantExpirationDate}}", placeHolders.GrantExpirationDate?.ToString("dddd, MMMM dd, yyyy")); //Grant Agreement Expiration Date from Property Admin details tab
        //Batch job

        //property
        //new placeholders

        if (primaryContact.Item2.Count() > 0)
        {
            toEmails = string.Join(",", primaryContact.Item2);
            //toEmails = systemParamOptions.IsDevelopment == false ? string.Join(",", primaryContact.Item2) : systemParamOptions.TestEmailIds;
        }
        else
        {
            toEmails = systemParamOptions.ProgramAdminEmail ?? String.Empty;
        }

        //appending Program admin to cc list
        if (primaryContact.Item3.Count() > 0)
        {
            alternateContactEmails.Add(string.Join(",", primaryContact.Item3));
        } 

        //to add program admin to cc list
        if (!toEmails.Contains(systemParamOptions.ProgramAdminEmail))
        {
            alternateContactEmails.Add(string.Join(",", systemParamOptions.ProgramAdminEmail));
        }

        //if contacts selectes then contact emails to cc list
        if (contactEmails.Count() > 0)
        {
            alternateContactEmails.Add(string.Join("", contactEmails));
        }

        var cc = string.Join(",", alternateContactEmails);

        if (emailTemplateCode == EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_IN_REVIEW_TO_ACTIVE.ToString())
        {
            toEmails = systemParamOptions.ProgramAdminEmail;
            cc = null;
        }

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
    Task SendMail(string subject, string htmlBody, int applicationId, string applicationName, string propertyName, int agencyId = default, DateTime? expirationDate = default, FloodEmailTemplatePlaceholdersEntity emailPlaceholders = default);
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
    public async Task SendMail(string subject, string htmlBody, int applicationId, string applicationName = "", string propertyName = "", int agencyId = default, DateTime? expirationDate = default, FloodEmailTemplatePlaceholdersEntity emailPlaceholders = default)
    {
        List<string> primaryContactEmails = new List<string>();
        List<string> primaryContactNames = new List<string>();
        string toEmails = string.Empty;
        List<string> alternateContactEmails = new List<string>();

        // get primary contacts
        var primaryContacts = await repoApplicationRole.GetPrimaryContactsAsync(applicationId);
        if (primaryContacts.Count() > 0)
        {
            primaryContactEmails = primaryContacts.Where(o => o.IsPrimaryContact).Select(i => i.Email).ToList();
            primaryContactNames = primaryContacts.Select(o => string.Format("{0}", o.UserName)).ToList();
            alternateContactEmails = primaryContacts.Where(o => o.IsAlternateContact).Select(i => i.Email).ToList();
        }

        alternateContactEmails.Add(string.Join("", systemParamOptions.ProgramAdminEmail));
        alternateContactEmails.Add(string.Join("", systemParamOptions.CC));//test


        htmlBody = htmlBody.Replace("{{ApplicationName}}", applicationName ?? "");
        htmlBody = htmlBody.Replace("{{ProjectName}}", applicationName ?? "");
        htmlBody = htmlBody.Replace("{{PrimaryContactName}}", string.Join(",", primaryContactNames));
        htmlBody = htmlBody.Replace("{{ProgramAdmin}}", systemParamOptions.ProgramAdminName);
        htmlBody = htmlBody.Replace("{{ProjectAreaExpirationDate}}", expirationDate?.ToString("dddd, MMMM dd, yyyy"));

        subject = subject.Replace("{{ApplicationName}}", applicationName ?? "");
        subject = subject.Replace("{{PropertyName}}", propertyName ?? "");


        if (primaryContactEmails.Count() > 0)
        {
            toEmails = systemParamOptions.IsDevelopment == false ? string.Join(",", primaryContactEmails) : systemParamOptions.TestEmailIds;
        }
        else
        {
            toEmails = systemParamOptions.ProgramAdminEmail;
        }

        var cc = string.Join(",", alternateContactEmails);
        
        var senderName = systemParamOptions.ProgramAdminName;
        var senderEmail = "mcgis@co.morris.nj.us";

        await this.Send(subject: subject, toEmails: toEmails, senderName: senderName, senderEmail: senderEmail, htmlBody: htmlBody, cc: cc);
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
