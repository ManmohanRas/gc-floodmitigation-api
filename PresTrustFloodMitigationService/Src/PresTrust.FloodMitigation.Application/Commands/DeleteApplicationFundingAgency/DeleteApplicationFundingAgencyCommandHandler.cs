namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationFundingAgencyCommandHandler: IRequestHandler<DeleteApplicationFundingAgencyCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationFundingAgencyRepository repoFundingAgency;
    private readonly IPresTrustUserContext userContext;

    public DeleteApplicationFundingAgencyCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationFundingAgencyRepository repoFundingAgency
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFundingAgency = repoFundingAgency;
    }

    public async Task<bool> Handle(DeleteApplicationFundingAgencyCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var fundingAgency = mapper.Map<DeleteApplicationFundingAgencyCommand, FloodApplicationFundingAgencyEntity>(request);

        await repoFundingAgency.DeleteAsync(fundingAgency);

        return true;
    }
}
