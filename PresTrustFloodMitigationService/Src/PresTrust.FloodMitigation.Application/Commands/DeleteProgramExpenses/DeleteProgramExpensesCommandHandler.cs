namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteProgramExpensesCommandHandler : IRequestHandler<DeleteProgramExpensesCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IProgramExpensesRepository repoExpenses;

    public DeleteProgramExpensesCommandHandler(
       IMapper mapper,
       IProgramExpensesRepository repoExpenses)
    {
        this.mapper = mapper;
        this.repoExpenses = repoExpenses;
    }

    public async Task<bool> Handle(DeleteProgramExpensesCommand request, CancellationToken cancellationToken)
    {
        var reqExpenses = mapper.Map<DeleteProgramExpensesCommand, FloodProgramExpensesEntity>(request);
        await repoExpenses.DeleteProgramExpensesAsync(reqExpenses);
        return true;
    }
}
