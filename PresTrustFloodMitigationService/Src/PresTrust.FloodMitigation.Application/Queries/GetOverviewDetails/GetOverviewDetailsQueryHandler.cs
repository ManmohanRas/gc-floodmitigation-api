namespace PresTrust.FloodMitigation.Application.Queries;

public class GetOverviewDetailsQueryHandler : IRequestHandler<GetOverviewDetailsQuery, GetOverviewDetailsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IOverviewDetailsRepository repoOverviewDetails;
    private readonly IFundingAgencyRepository repoFundingAgency;
    public GetOverviewDetailsQueryHandler(
             IMapper mapper,
             IOverviewDetailsRepository repoOverviewDetails,
             IFundingAgencyRepository repoFundingAgency)
    {
        this.mapper = mapper;
        this.repoOverviewDetails = repoOverviewDetails;
        this.repoFundingAgency = repoFundingAgency;
    }

    public async Task<GetOverviewDetailsQueryViewModel> Handle(GetOverviewDetailsQuery request, CancellationToken cancellationToken)
    {
        FloodOverviewDetailsEntity results = default;

        results = await this.repoOverviewDetails.GetOverviewDetailsAsync(request.ApplicationId);

        var overviewDetails = mapper.Map<FloodOverviewDetailsEntity, GetOverviewDetailsQueryViewModel>(results);

        var fundingAgencies = await repoFundingAgency.GetFundingAgencies(request.ApplicationId);

        overviewDetails.FundingAgencies = mapper.Map<IEnumerable<FloodFundingAgencyEntity>, IEnumerable<FloodFundingAgencyViewModel>>(fundingAgencies);

        return overviewDetails;
    }
}
