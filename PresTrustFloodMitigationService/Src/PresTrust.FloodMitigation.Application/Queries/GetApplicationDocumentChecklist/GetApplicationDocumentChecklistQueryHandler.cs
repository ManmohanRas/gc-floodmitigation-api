namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationDocumentChecklistQueryHandler : BaseHandler, IRequestHandler<GetApplicationDocumentChecklistQuery, IEnumerable<ApplicationDocumentChecklistSectionViewModel>>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationRepository repoApplication;
    //private readonly ISiteRepository repoSite;
    private readonly IApplicationDocumentRepository repoDocuments;

    public GetApplicationDocumentChecklistQueryHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationRepository repoApplication,
        IApplicationDocumentRepository repoDocuments
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoApplication = repoApplication;
        this.repoDocuments = repoDocuments;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ApplicationDocumentChecklistSectionViewModel>> Handle(GetApplicationDocumentChecklistQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get documents 
        var documents = await repoDocuments.GetApplicationDocumentChecklistAsync(request.ApplicationId);

        // build checklist view model
        var docBuilder = new ApplicationDocumentTreeBuilder(documents, buildChecklist: true);
        return docBuilder.documentChecklistItems;
    }
}
