namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalFinanceCommandHandler : IRequestHandler<SaveMunicipalFinanceCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IMunicipalFinanceRepository repoFinance;

    public SaveMunicipalFinanceCommandHandler(
            IMapper mapper,
            IPresTrustUserContext userContext,
            IMunicipalFinanceRepository repoFinance
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFinance = repoFinance;
    }
    public async Task<int> Handle(SaveMunicipalFinanceCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var muncipalFinance = mapper.Map<SaveMunicipalFinanceCommand, FloodMunicipalFinanceEntity>(request);
        int id = await repoFinance.SaveMunicipalFinanceDetailsAsync(muncipalFinance);
        
        return id;
    }
}
