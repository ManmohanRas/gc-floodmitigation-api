namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftCostDetailsQueryHandler : BaseHandler, IRequestHandler<GetSoftCostDetailsQuery, GetSoftCostDetailsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IFinanceRepository repoFinance;
    private readonly ISoftCostRepository repoSoftCost;
    private readonly IPropertyDocumentRepository repoDocument;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    public GetSoftCostDetailsQueryHandler(
         IMapper mapper,
         IFinanceRepository repoFinance,
         IApplicationRepository repoApplication,
         IPropertyDocumentRepository repoDocument,
         ISoftCostRepository repoSoftCost,
         IApplicationParcelRepository repoApplicationParcel) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoFinance = repoFinance;
        this.repoSoftCost = repoSoftCost;
        this.repoApplication = repoApplication;
        this.repoDocument = repoDocument;
        this.repoApplicationParcel = repoApplicationParcel;
    }

    public async Task<GetSoftCostDetailsQueryViewModel> Handle(GetSoftCostDetailsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var finance = await repoFinance.GetFinanceAsync(application.Id);
        var softCostLineItems = await repoSoftCost.GetAllSoftCostLineItemsAsync(application.Id, request.PamsPin);
        var softCosts = mapper.Map<IEnumerable<FloodParcelSoftCostEntity>, IEnumerable<FloodParcelSoftCostViewModel>>(softCostLineItems);
        var documents = await GetPropertyDocument(application.Id, request.PamsPin);
        var appParcel = await repoApplicationParcel.GetApplicationPropertyAsync(application.Id, request.PamsPin);

        var result = new GetSoftCostDetailsQueryViewModel()
        {
            ApplicationId = request.ApplicationId,
            PamsPin = request.PamsPin,
            CostShare = finance.MatchPercent,
            SoftCostLineItems = softCosts,
            IsSubmitted = appParcel.IsSubmitted,
            IsApproved = appParcel.IsApproved,
            DocumentsTree = documents ?? new List<PropertyDocumentTypeViewModel>()
        };
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    private async Task<List<PropertyDocumentTypeViewModel>> GetPropertyDocument(int applicationId, string pamsPin)
    {
        var documents = await repoDocument.GetPropertyDocumentsAsync(applicationId, pamsPin, (int)PropertySectionEnum.SOFT_COSTS);

        List<PropertyDocumentTypeViewModel> documentsTree = new List<PropertyDocumentTypeViewModel>();
        if (documents != null)
        {
            var docBuilder = new PropertyDocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }

        return documentsTree;
    }

}
