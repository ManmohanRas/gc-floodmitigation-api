namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetApplicationFinanceDetailsQueryHandler : IRequestHandler<GetApplicationFinanceDetailsQuery, GetApplicationFinanceDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPresTrustUserContext userContext;
    private readonly IFinanceRepository repoFinance;
    private readonly IFundingSourceRepoitory repoFundingSource;
 

    public GetApplicationFinanceDetailsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IFinanceRepository repoFinance,
        IFundingSourceRepoitory repoFundingSource
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoFinance = repoFinance;
        this.repoFundingSource = repoFundingSource;
    }
    public async Task<GetApplicationFinanceDetailsQueryViewModel> Handle(GetApplicationFinanceDetailsQuery request, CancellationToken cancellationToken)
    {
        var reqFinance = await repoFinance.GetFinanceAsync(request.ApplicationId);

        var finance = mapper.Map<FloodApplicationFinanceEntity,GetApplicationFinanceDetailsQueryViewModel>(reqFinance);

        finance = finance ?? new GetApplicationFinanceDetailsQueryViewModel() { ApplicationId = request.ApplicationId };

        var fundingSources = await repoFundingSource.GetFundingSourcesAsync(request.ApplicationId);

        var result = new GetApplicationFinanceDetailsQueryViewModel()
        {
            ApplicationId = request.ApplicationId,
            MatchPercent = finance.MatchPercent,
            FundingSources = mapper.Map<IEnumerable<FloodFundingSourceEntity>, IEnumerable<FloodFundingSourceViewModel>>(fundingSources),
            FinanceLineItems = new List<FloodFinanceLineItemViewModel>() { }
        };

        return result;
    }
}
   