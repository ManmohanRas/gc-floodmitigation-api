namespace PresTrust.FloodMitigation.Application.Commands;

public class EmailTriggerCommandHandler : BaseHandler, IRequestHandler<EmailTriggerCommand, bool>
{
    private readonly IEmailManager repoEmailManager;
    private readonly IApplicationRepository repoApplication;

    public EmailTriggerCommandHandler
        (
        IEmailManager repoEmailManager,
        IApplicationRepository repoApplication
        ) : base(repoApplication)
    {
        this.repoEmailManager = repoEmailManager;
        this.repoApplication = repoApplication;
    }

    public async Task<bool> Handle(EmailTriggerCommand request, CancellationToken cancellationToken)
    {
        FloodApplicationEntity application = new FloodApplicationEntity() { AgencyId = request.AgencyId };
        FloodApplicationParcelEntity parcel = new FloodApplicationParcelEntity();

        // check if application exists
        if ((bool)!request.IsProgramManager)
        {
            application = await GetIfApplicationExists(request.ApplicationId);
        }

        //Get Template and Send Email
        await repoEmailManager.GetEmailTemplate(request.EmailTemplateCode, application, parcel, request.EmailDate);

        return true;
    }
}
