namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationOverviewCommandHandler : IRequestHandler<SaveApplicationOverviewCommand, int>
{
    private readonly IMapper mapper;
    private readonly IApplicationOverviewRepository repoOverviewDetails;
    private readonly IPresTrustUserContext userContext;

    public SaveApplicationOverviewCommandHandler
        (
        IMapper mapper,
        IApplicationOverviewRepository repoOverviewDetails,
        IPresTrustUserContext userContext
        )
    {
        this.mapper = mapper;
        this.repoOverviewDetails = repoOverviewDetails;
        this.userContext = userContext;
    }
    public async Task<int> Handle(SaveApplicationOverviewCommand request, CancellationToken cancellationToken)
    {
        var reqOverviewDetails = mapper.Map<SaveApplicationOverviewCommand,FloodApplicationOverviewEntity>(request);

        reqOverviewDetails.LastUpdatedBy = userContext.Email;
      
        FloodApplicationOverviewEntity overviewDetails = default;

        overviewDetails = await repoOverviewDetails.SaveAsync(reqOverviewDetails);

        return overviewDetails.Id;
    }
}
