namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFundingAgencyCommandHandler: IRequestHandler<DeleteFundingAgencyCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IFundingAgencyRepository repoFundingAgency;

    public DeleteFundingAgencyCommandHandler(
        IMapper mapper,
        IFundingAgencyRepository repoFundingAgency
        )
    {
        this.mapper = mapper;
        this.repoFundingAgency = repoFundingAgency;
    }

    public async Task<bool> Handle(DeleteFundingAgencyCommand request, CancellationToken cancellationToken)
    {
        var fundingAgency = mapper.Map<DeleteFundingAgencyCommand, FloodFundingAgencyEntity>(request);

        await repoFundingAgency.DeleteAsync(fundingAgency);

        return true;
    }
}
