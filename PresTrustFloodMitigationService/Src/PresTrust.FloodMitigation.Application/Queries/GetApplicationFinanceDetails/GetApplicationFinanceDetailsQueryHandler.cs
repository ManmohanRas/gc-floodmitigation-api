namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetApplicationFinanceDetailsQueryHandler : BaseHandler, IRequestHandler<GetApplicationFinanceDetailsQuery, GetApplicationFinanceDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IFinanceRepository repoFinance;
    private readonly IFundingSourceRepoitory repoFundingSource;
    private readonly IFinanceLineItemRepository repoFinanceLineItem;
    private readonly IApplicationRepository repoApplication;


    public GetApplicationFinanceDetailsQueryHandler(
        IMapper mapper,
        IFinanceRepository repoFinance,
        IFundingSourceRepoitory repoFundingSource,
        IFinanceLineItemRepository repoFinanceLineItem,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoFinance = repoFinance;
        this.repoFundingSource = repoFundingSource;
        this.repoFinanceLineItem = repoFinanceLineItem;
        this.repoApplication = repoApplication;
    }
    public async Task<GetApplicationFinanceDetailsQueryViewModel> Handle(GetApplicationFinanceDetailsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var reqFinance = await repoFinance.GetFinanceAsync(application.Id);

        var finance = mapper.Map<FloodApplicationFinanceEntity,GetApplicationFinanceDetailsQueryViewModel>(reqFinance);

        //get funding sources
        var fundingSources = await repoFundingSource.GetFundingSourcesAsync(application.Id);

        //get finance line items
        var parcelFinance = await repoFinanceLineItem.GetFinanceLineItemsAsync(application.Id);

        var result = new GetApplicationFinanceDetailsQueryViewModel()
        {
            ApplicationId = application.Id,
            Id = finance.Id,
            MatchPercent = finance.MatchPercent,
            FundingSources = mapper.Map<IEnumerable<FloodFundingSourceEntity>, IEnumerable<FloodFundingSourceViewModel>>(fundingSources),
            FinanceLineItems = mapper.Map<IEnumerable<FloodFinanceLineItemEntity>, IEnumerable<FloodFinanceLineItemViewModel>>(parcelFinance)
        };
        result.FinanceLineItems = result.FinanceLineItems.OrderBy(o => o.PamsPin);

        return result;
    }
}
   