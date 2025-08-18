namespace PresTrust.FloodMitigation.Application.Queries;

public class GetProgramExpensesQueryHandler : IRequestHandler<GetProgramExpensesQuery, IEnumerable<GetProgramExpensesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IProgramExpensesRepository repoExpenses;
    private readonly IPresTrustUserContext userContext;


    public GetProgramExpensesQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IProgramExpensesRepository repoExpenses)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoExpenses = repoExpenses;
    }

    public async Task<IEnumerable<GetProgramExpensesQueryViewModel>> Handle(GetProgramExpensesQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var expensesList = await repoExpenses.GetAllProgramExpensesAsync();
        var existingYears = expensesList.Select(o => int.Parse(o.ExpenseYear)).Distinct().ToList();

        List<FloodProgramExpensesEntity> reqExpenses = new();
        int currentYear = DateTime.Now.Year;
        for(var i = 2012; i <= currentYear; i++)
        {
            if(existingYears.Contains(i))
            {
                reqExpenses.AddRange(expensesList.Where(o => o.ExpenseYear == i.ToString()));
            }
            else
            {
                reqExpenses.Add(new()
                {
                    Id = 0,
                    ExpenseYear = i.ToString(),
                    ExpenseAmount = 0
                });
            }
        }
        var expenses = mapper.Map<IEnumerable<FloodProgramExpensesEntity>, IEnumerable<GetProgramExpensesQueryViewModel>>(reqExpenses.OrderByDescending(o => o.ExpenseYear));

        return expenses;
    }
}
