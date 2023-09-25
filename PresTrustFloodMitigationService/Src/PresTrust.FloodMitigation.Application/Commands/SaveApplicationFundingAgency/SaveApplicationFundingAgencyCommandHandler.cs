namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationFundingAgencyCommandHandler : IRequestHandler<SaveApplicationFundingAgencyCommand, int>
{
    private readonly IMapper mapper;
    private readonly IApplicationFundingAgencyRepository repoFundingAgency;

    public SaveApplicationFundingAgencyCommandHandler(
        IMapper mapper,
        IApplicationFundingAgencyRepository repoFundingAgency
        )
    {
        this.mapper = mapper;
        this.repoFundingAgency  = repoFundingAgency;
    }
    public async Task<int> Handle(SaveApplicationFundingAgencyCommand request, CancellationToken cancellationToken)
    {
        var fundingAgency = mapper.Map<SaveApplicationFundingAgencyCommand, FloodApplicationFundingAgencyEntity>(request);

        fundingAgency = await repoFundingAgency.SaveAsync(fundingAgency);

        return fundingAgency.Id;

    }
}
