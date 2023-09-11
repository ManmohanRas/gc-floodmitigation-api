namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactCommandHandler : IRequestHandler<SaveContactCommand,int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IContactRepository repoContact;

    public SaveContactCommandHandler
        (
         IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IContactRepository repoContact
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoContact = repoContact;
    }

    public async Task<int> Handle(SaveContactCommand request, CancellationToken cancellationToken)
    {
        var reqContact = mapper.Map<SaveContactCommand, FloodContactEntity>(request);


        // save contact
        FloodContactEntity contact = default;
        contact = await this.repoContact.SaveAsync(reqContact);

        return contact.Id;
    }
}
