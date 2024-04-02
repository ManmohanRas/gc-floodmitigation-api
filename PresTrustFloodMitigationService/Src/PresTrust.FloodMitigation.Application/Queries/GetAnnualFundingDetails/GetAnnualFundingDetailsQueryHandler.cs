namespace PresTrust.FloodMitigation.Application.Queries;

public class GetAnnualFundingDetailsQueryHandler : IRequestHandler<GetAnnualFundingDetailsQuery, IEnumerable<GetAnnualFundingDetailsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IAnnualFundingAmountsRepository repoFunding;

    public GetAnnualFundingDetailsQueryHandler(
         IMapper mapper,
         IAnnualFundingAmountsRepository repoContact)
    {
        this.mapper = mapper;
        this.repoFunding = repoContact;
    }

    public async Task<IEnumerable<GetAnnualFundingDetailsQueryViewModel>> Handle(GetAnnualFundingDetailsQuery request, CancellationToken cancellationToken)
    {
        var reqFunds = await repoFunding.GetFundingDetailsAsync();
        if(reqFunds?.Count > 0)
        {
            reqFunds = reqFunds.OrderByDescending(o => o.AllocationYear).ToList();
        }
        var fundings = mapper.Map<IEnumerable<FloodAnnualFundingEntity>, IEnumerable<GetAnnualFundingDetailsQueryViewModel>>(reqFunds);
        return fundings;
    }
}