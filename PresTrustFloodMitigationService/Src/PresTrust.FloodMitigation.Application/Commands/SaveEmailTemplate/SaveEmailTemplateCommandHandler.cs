namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveEmailTemplateCommandHandler: IRequestHandler<SaveEmailTemplateCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IEmailTemplateRepository repoEmailTemplate;
    public SaveEmailTemplateCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IEmailTemplateRepository repoEmailTemplate
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoEmailTemplate = repoEmailTemplate;
    }

    public async Task<int> Handle(SaveEmailTemplateCommand request, CancellationToken cancellationToken)
    {
        FloodEmailTemplateEntity emailTemplate = default;

        // map command object to the HistEmailTemplateEntity
        var reqTemplate = mapper.Map<SaveEmailTemplateCommand, FloodEmailTemplateEntity>(request);
        reqTemplate.LastUpdatedBy = userContext.Email;

        emailTemplate = await repoEmailTemplate.SaveAsync(reqTemplate);

        return emailTemplate.Id;

    }
}
