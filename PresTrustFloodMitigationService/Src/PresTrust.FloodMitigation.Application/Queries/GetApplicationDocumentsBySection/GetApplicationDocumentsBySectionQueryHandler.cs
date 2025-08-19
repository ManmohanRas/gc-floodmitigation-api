namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetApplicationDocumentsBySectionQueryHandler : BaseHandler, IRequestHandler<GetApplicationDocumentsBySectionQuery, IEnumerable<ApplicationDocumentTypeViewModel>>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationDocumentRepository repoDocument;
    private readonly IApplicationRepository repoApplication;
    public GetApplicationDocumentsBySectionQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationDocumentRepository repoDocument,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoDocument = repoDocument;
        this.repoApplication = repoApplication;
    }
    public async Task<IEnumerable<ApplicationDocumentTypeViewModel>> Handle(GetApplicationDocumentsBySectionQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        Enum.TryParse(value: request.SectionName, ignoreCase: true, out ApplicationSectionEnum applicationSection);
        var documents = await repoDocument.GetApplicationDocumentsAsync(application.Id, (int)applicationSection);

        List<ApplicationDocumentTypeViewModel>? documentsTree = default;

        if (documents.Count() > 0)
        {
            var docBuilder = new ApplicationDocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }
        else
        {
            documentsTree = new List<ApplicationDocumentTypeViewModel>();
        }

        return documentsTree;
    }
}
