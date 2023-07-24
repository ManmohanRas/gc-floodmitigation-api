namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetDocumentsBySectionDetailsQueryHandler : BaseHandler, IRequestHandler<GetDocumentsBySectionDetailsQuery, IEnumerable<DocumentTypeViewModel>>
{
    private readonly IMapper mapper;
    private readonly IDocumentRepository repoDocument;
    private readonly IApplicationRepository repoApplication;
    public GetDocumentsBySectionDetailsQueryHandler(
        IMapper mapper,
        IDocumentRepository repoDocument,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoDocument = repoDocument;
        this.repoApplication = repoApplication;
    }
    public async Task<IEnumerable<DocumentTypeViewModel>> Handle(GetDocumentsBySectionDetailsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        Enum.TryParse(value: request.SectionName, ignoreCase: true, out ApplicationSectionEnum applicationSection);
        var documents = await repoDocument.GetDocumentsAsync(application.Id, (int)applicationSection);

        List<DocumentTypeViewModel>? documentsTree = default;

        if (documents.Count() > 0)
        {
            var docBuilder = new DocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }
        else
        {
            documentsTree = new List<DocumentTypeViewModel>();
        }

        return documentsTree;
    }
}
