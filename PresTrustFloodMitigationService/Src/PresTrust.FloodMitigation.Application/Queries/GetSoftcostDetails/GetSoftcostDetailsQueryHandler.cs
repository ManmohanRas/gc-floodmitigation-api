namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftCostDetailsQueryHandler : IRequestHandler<GetSoftCostDetailsQuery, GetSoftCostDetailsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IFinanceRepository repoFinance;
    private readonly ISoftCostRepository repoSoftCost;
    public GetSoftCostDetailsQueryHandler(
         IMapper mapper,
         IFinanceRepository repoFinance,
         ISoftCostRepository repoSoftCost)
    {
        this.mapper = mapper;
        this.repoFinance = repoFinance;
        this.repoSoftCost = repoSoftCost;
    }

    public async Task<GetSoftCostDetailsQueryViewModel> Handle(GetSoftCostDetailsQuery request, CancellationToken cancellationToken)
    {
        var finance = await repoFinance.GetFinanceAsync(request.ApplicationId);
        var softCostLineItems = await repoSoftCost.GetAllSoftCostLineItemsAsync(request.ApplicationId, request.PamsPin);
        var softCosts = mapper.Map<IEnumerable<FloodParcelSoftCostEntity>, IEnumerable<FloodParcelSoftCostViewModel>>(softCostLineItems);
        var result = new GetSoftCostDetailsQueryViewModel()
        {
            ApplicationId = request.ApplicationId,
            PamsPin = request.PamsPin,
            CostShare = finance.MatchPercent,
            SoftCostLineItems = softCosts
        };
        return result;
    }
}
