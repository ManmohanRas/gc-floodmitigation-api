namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryHandler : IRequestHandler<GetProgramExpensesQuery, IEnumerable<GetProgramExpensesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IProgramExpensesRepository repoExpenses;


    public GetProgramExpensesQueryHandler(
        IMapper mapper,
        IProgramExpensesRepository repoExpenses)
    {
        this.mapper = mapper;
        this.repoExpenses = repoExpenses;
    }

    public async Task<IEnumerable<GetProgramExpensesQueryViewModel>> Handle(GetProgramExpensesQuery request, CancellationToken cancellationToken)
    {
        var reqExpenses = await repoExpenses.GetAllProgramExpensesAsync();

        var expenses = mapper.Map<IEnumerable<FloodProgramExpensesEntity>, IEnumerable<GetProgramExpensesQueryViewModel>>(reqExpenses);

        return expenses;
    }
}
