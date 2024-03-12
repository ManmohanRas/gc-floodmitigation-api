namespace PresTrust.FloodMitigation.Application.Queries;
public class GetAllEmailTemplatesQueryHandler : IRequestHandler<GetAllEmailTemplatesQuery, IEnumerable<GetAllEmailTemplatesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IEmailTemplateRepository repoEmailTemplate;

    public GetAllEmailTemplatesQueryHandler(
        IMapper mapper,
        IEmailTemplateRepository repoEmailTemplate
        )
    {
        this.mapper = mapper;
        this.repoEmailTemplate = repoEmailTemplate;
    }
    public async Task<IEnumerable<GetAllEmailTemplatesQueryViewModel>> Handle(GetAllEmailTemplatesQuery request, CancellationToken cancellationToken)
    {
        //get email template details
        var templates = await repoEmailTemplate.GetAllEmailTemplates();
        var result = mapper.Map<IEnumerable<FloodEmailTemplateEntity>, IEnumerable<GetAllEmailTemplatesQueryViewModel>>(templates);

        return result;
    }
}
