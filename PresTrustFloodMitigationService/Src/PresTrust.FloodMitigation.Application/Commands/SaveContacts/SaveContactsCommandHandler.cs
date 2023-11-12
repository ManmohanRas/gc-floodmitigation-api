namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactsCommandHandler : BaseHandler, IRequestHandler<SaveContactsCommand,bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IContactRepository repoContact;

    public SaveContactsCommandHandler
        (
         IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IContactRepository repoContact
        ) : base (repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoContact = repoContact;
    }

    public async Task<bool> Handle(SaveContactsCommand request, CancellationToken cancellationToken)
    {
        var application = await GetIfApplicationExists(request.ApplicationId);

        foreach(var contact in request.Contacts)
        {
            var reqContact = mapper.Map<SaveContactsModel, FloodContactEntity>(contact);
            reqContact.ApplicationId = application.Id;
            reqContact.LastUpdatedBy = userContext.Email;

            await this.repoContact.SaveAsync(reqContact);
        }

        return true;
    }
}
