namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalFinanceCommandHandler : IRequestHandler<SaveMunicipalFinanceCommand, int>
{
    private readonly IMapper mapper;
    private readonly IMunicipalFinanceRepository repoFinance;

    public SaveMunicipalFinanceCommandHandler(
            IMapper mapper,
            IMunicipalFinanceRepository repoFinance
        )
    {
        this.mapper = mapper;
        this.repoFinance = repoFinance;
    }
    public async Task<int> Handle(SaveMunicipalFinanceCommand request, CancellationToken cancellationToken)
    {
        var muncipalFinance = mapper.Map<SaveMunicipalFinanceCommand, FloodMunicipalFinanceEntity>(request);
        int id = await repoFinance.SaveMunicipalFinanceDetailsAsync(muncipalFinance);
        
        return id;
    }
}
