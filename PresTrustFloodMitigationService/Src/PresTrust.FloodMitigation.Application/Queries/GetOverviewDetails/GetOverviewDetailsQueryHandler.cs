namespace PresTrust.FloodMitigation.Application.Queries;

public class GetOverviewDetailsQueryHandler : IRequestHandler<GetOverviewDetailsQuery, GetOverviewDetailsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IOverviewDetailsRepository repoOverviewDetails;
    public GetOverviewDetailsQueryHandler(
             IMapper mapper,
             IOverviewDetailsRepository repoOverviewDetails)
    {
        this.mapper = mapper;
        this.repoOverviewDetails = repoOverviewDetails;
    }

    public async Task<GetOverviewDetailsQueryViewModel> Handle(GetOverviewDetailsQuery request, CancellationToken cancellationToken)
    {
        FloodOverviewDetailsEntity results = default;

        results = await this.repoOverviewDetails.GetOverviewDetailsAsync(request.ApplicationId);

        var overviewDetails = mapper.Map<FloodOverviewDetailsEntity, GetOverviewDetailsQueryViewModel>(results);

        return overviewDetails;
    }
}
