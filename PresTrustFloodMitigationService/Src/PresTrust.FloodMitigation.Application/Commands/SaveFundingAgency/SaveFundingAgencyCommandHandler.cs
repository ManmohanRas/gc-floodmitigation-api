namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFundingAgencyCommandHandler : IRequestHandler<SaveFundingAgencyCommand, int>
{
    private readonly IMapper mapper;
    private readonly IFundingAgencyRepository repoFundingAgency;

    public SaveFundingAgencyCommandHandler(
        IMapper mapper,
        IFundingAgencyRepository repoFundingAgency
        )
    {
        this.mapper = mapper;
        this.repoFundingAgency  = repoFundingAgency;
    }
    public async Task<int> Handle(SaveFundingAgencyCommand request, CancellationToken cancellationToken)
    {
        var fundingAgency = mapper.Map<SaveFundingAgencyCommand, FloodFundingAgencyEntity>(request);

        fundingAgency = await repoFundingAgency.SaveAsync(fundingAgency);

        return fundingAgency.Id;

    }
}
