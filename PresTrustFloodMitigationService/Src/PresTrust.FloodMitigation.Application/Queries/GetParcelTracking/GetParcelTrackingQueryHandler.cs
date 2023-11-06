namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelTrackingQueryHandler : BaseHandler, IRequestHandler<GetParcelTrackingQuery, GetParcelTrackingQueryViewModel>
{
    private IMapper mapper;
    private IApplicationRepository repoApplication;
    private IParcelTrackingRepository repoParcelTracking;
    private IPropertyDocumentRepository repoDocument;
    public GetParcelTrackingQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IParcelTrackingRepository repoParcelTracking,
        IPropertyDocumentRepository repoDocument

        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoParcelTracking = repoParcelTracking;
        this.repoDocument = repoDocument;
    }

    public async Task<GetParcelTrackingQueryViewModel> Handle(GetParcelTrackingQuery request, CancellationToken cancellationToken)
    {
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get parcel Tracking
        var parcelTracking = await this.repoParcelTracking.GetTrackingAsync(request.ApplicationId, request.PamsPin);
        var documents = await GetPropertyDocument(request.ApplicationId, request.PamsPin);
        var result = mapper.Map<FloodParcelTrackingEntity, GetParcelTrackingQueryViewModel>(parcelTracking);
        result.DocumentsTree = documents ?? new List<PropertyDocumentTypeViewModel>();
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    private async Task<List<PropertyDocumentTypeViewModel>> GetPropertyDocument(int applicationId, string pamsPin)
    {
        var documents = await repoDocument.GetPropertyDocumentsAsync(applicationId, (int)PropertySectionEnum.ADMIN_TRACKING, pamsPin);

        List<PropertyDocumentTypeViewModel> documentsTree = new List<PropertyDocumentTypeViewModel>();
        if (documents != null)
        {
            var docBuilder = new PropertyDocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }

        return documentsTree;
    }




}
