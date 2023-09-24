namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationOverviewQueryHandler : IRequestHandler<GetApplicationOverviewQuery, GetApplicationOverviewQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IApplicationOverviewRepository repoOverviewDetails;
    private readonly IApplicationFundingAgencyRepository repoFundingAgency;
    public GetApplicationOverviewQueryHandler(
             IMapper mapper,
             IApplicationOverviewRepository repoOverviewDetails,
             IApplicationFundingAgencyRepository repoFundingAgency)
    {
        this.mapper = mapper;
        this.repoOverviewDetails = repoOverviewDetails;
        this.repoFundingAgency = repoFundingAgency;
    }

    public async Task<GetApplicationOverviewQueryViewModel> Handle(GetApplicationOverviewQuery request, CancellationToken cancellationToken)
    {
        FloodApplicationOverviewEntity results = default;

        results = await this.repoOverviewDetails.GetOverviewDetailsAsync(request.ApplicationId);

        var overviewDetails = mapper.Map<FloodApplicationOverviewEntity, GetApplicationOverviewQueryViewModel>(results);

        var fundingAgencies = await repoFundingAgency.GetFundingAgencies(request.ApplicationId);

        overviewDetails.FundingAgencies = mapper.Map<IEnumerable<FloodApplicationFundingAgencyEntity>, IEnumerable<FloodApplicationFundingAgencyViewModel>>(fundingAgencies);

        return overviewDetails;
    }
}
