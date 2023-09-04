namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftcostDetailsQueryHandler : IRequestHandler<GetSoftcostDetailsQuery, GetSoftcostDetailsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly ISoftcostRepository repoSoftcost;
    public GetSoftcostDetailsQueryHandler(
         IMapper mapper,
         ISoftcostRepository repoSoftcost)
    {
        this.mapper = mapper;
        this.repoSoftcost = repoSoftcost;
    }

    public async Task<GetSoftcostDetailsQueryViewModel> Handle(GetSoftcostDetailsQuery request, CancellationToken cancellationToken)
    {

        var softCostLineItems = await this.repoSoftcost.GetAllSoftcostLineItemsAsync(request.ApplicationId, request.PamsPin);

        var softCosts = mapper.Map<IEnumerable<FloodParcelSoftcostEntity>, IEnumerable<FloodParcelSoftcostViewModel>>(softCostLineItems);

        foreach (var item in softCosts)
        {
            item.SoftcostTotal = item.PaymentAmount * item.CostShare;
        }
        var result = new GetSoftcostDetailsQueryViewModel()
        {
            ApplicationId = request.ApplicationId,
            SoftcostLineItems = softCosts
        };

        return result;
    }
}
