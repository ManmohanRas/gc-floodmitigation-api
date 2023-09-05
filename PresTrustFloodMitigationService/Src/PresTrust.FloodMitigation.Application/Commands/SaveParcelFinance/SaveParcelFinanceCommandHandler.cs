namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelFinanceCommandHandler : IRequestHandler<SaveParcelFinanceCommand, int>
{
    private IMapper mapper;
    private IParcelFinanceRepository repoParcelFinance;

    public SaveParcelFinanceCommandHandler(
        IMapper mapper
       ,IParcelFinanceRepository repoParcelFinance
        )
    {
        this.mapper = mapper;
        this.repoParcelFinance = repoParcelFinance;
    }
    public async Task<int> Handle(SaveParcelFinanceCommand request, CancellationToken cancellationToken)
    {
        var reqParcelFinance = mapper.Map<SaveParcelFinanceCommand, FloodParcelFinanceEntity>(request);
        reqParcelFinance = await repoParcelFinance.SaveAsync(reqParcelFinance);

        return reqParcelFinance.Id;
    }
}
