namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveOverviewDetailsCommandHandler : IRequestHandler<SaveOverviewDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IOverviewDetailsRepository repoOverviewDetails;
    private readonly IPresTrustUserContext userContext;

    public SaveOverviewDetailsCommandHandler
        (
        IMapper mapper,
        IOverviewDetailsRepository repoOverviewDetails,
        IPresTrustUserContext userContext
        )
    {
        this.mapper = mapper;
        this.repoOverviewDetails = repoOverviewDetails;
        this.userContext = userContext;
    }
    public async Task<int> Handle(SaveOverviewDetailsCommand request, CancellationToken cancellationToken)
    {
        var reqOverviewDetails = mapper.Map<SaveOverviewDetailsCommand,FloodOverviewDetailsEntity>(request);

        reqOverviewDetails.LastUpdatedBy = userContext.Email;
      
        FloodOverviewDetailsEntity overviewDetails = default;

        overviewDetails = await repoOverviewDetails.SaveAsync(reqOverviewDetails);

        return overviewDetails.Id;
    }
}
