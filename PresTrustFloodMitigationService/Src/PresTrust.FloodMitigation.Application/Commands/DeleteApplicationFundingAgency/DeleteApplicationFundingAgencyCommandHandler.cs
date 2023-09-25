namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationFundingAgencyCommandHandler: IRequestHandler<DeleteApplicationFundingAgencyCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationFundingAgencyRepository repoFundingAgency;

    public DeleteApplicationFundingAgencyCommandHandler(
        IMapper mapper,
        IApplicationFundingAgencyRepository repoFundingAgency
        )
    {
        this.mapper = mapper;
        this.repoFundingAgency = repoFundingAgency;
    }

    public async Task<bool> Handle(DeleteApplicationFundingAgencyCommand request, CancellationToken cancellationToken)
    {
        var fundingAgency = mapper.Map<DeleteApplicationFundingAgencyCommand, FloodApplicationFundingAgencyEntity>(request);

        await repoFundingAgency.DeleteAsync(fundingAgency);

        return true;
    }
}
