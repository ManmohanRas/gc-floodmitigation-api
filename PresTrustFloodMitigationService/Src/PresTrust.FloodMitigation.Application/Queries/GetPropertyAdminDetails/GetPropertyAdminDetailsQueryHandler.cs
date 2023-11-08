using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyAdminDetailsQueryHandler : BaseHandler, IRequestHandler<GetPropertyAdminDetailsQuery, GetPropertyAdminDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IPropertyAdminDetailsRepository PropDetails;
    private readonly IPropertyDocumentRepository repoDocument;

    public GetPropertyAdminDetailsQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IPropertyAdminDetailsRepository PropDetails,
            IPropertyDocumentRepository repoDocument
        ) :base (repoApplication:repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.PropDetails = PropDetails;
        this.repoDocument = repoDocument;
    }

    public async Task<GetPropertyAdminDetailsQueryViewModel> Handle(GetPropertyAdminDetailsQuery request, CancellationToken cancellationToken)
    {
        var propertyDetails = await PropDetails.GetAsync(request.ApplicationId, request.PamsPin);
        var documents = await GetPropertyDocument(request.ApplicationId, request.PamsPin);

        var result = mapper.Map<FloodPropertyAdminDetailsEntity, GetPropertyAdminDetailsQueryViewModel>(propertyDetails);
        result.DocumentsTree = documents ?? new List<PropertyDocumentTypeViewModel>();
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    private async Task<List<PropertyDocumentTypeViewModel>> GetPropertyDocument(int applicationId, string PamsPin)
    {
        var documents = await repoDocument.GetPropertyDocumentsAsync(applicationId, (int)PropertySectionEnum.ADMIN_DETAILS, PamsPin);

        List<PropertyDocumentTypeViewModel> documentsTree = new List<PropertyDocumentTypeViewModel>();
        if (documents != null)
        {
            var docBuilder = new PropertyDocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }

        return documentsTree;
    }
}
