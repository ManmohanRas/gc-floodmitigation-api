namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyDocumentChecklistQueryHandler : BaseHandler, IRequestHandler<GetPropertyDocumentChecklistQuery, IEnumerable<PropertyDocumentChecklistSectionViewModel>>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationRepository repoApplication;
    private readonly IPropertyDocumentRepository repoDocuments;
   
    public GetPropertyDocumentChecklistQueryHandler
    (
        IMapper mapper,
        IApplicationRepository repoApplication,
        IPresTrustUserContext userContext,
        IPropertyDocumentRepository repoDocuments
    ) : base(repoApplication: repoApplication)
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
    public async Task<IEnumerable<PropertyDocumentChecklistSectionViewModel>> Handle(GetPropertyDocumentChecklistQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get documents 
        var documents = await repoDocuments.GetPropertyDocumentChecklistAsync(request.ApplicationId, request.PamsPin);

        // build checklist view model
        var docBuilder = new PropertyDocumentTreeBuilder(documents, buildPropChecklist: true);
        return docBuilder.documentChecklistItems;
    }
}
