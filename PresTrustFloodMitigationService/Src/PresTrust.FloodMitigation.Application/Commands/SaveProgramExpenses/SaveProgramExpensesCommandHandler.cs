namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramExpensesCommandHandler : IRequestHandler<SaveProgramExpensesCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IProgramExpensesRepository repoExpenses;

    public SaveProgramExpensesCommandHandler
        (
         IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IProgramExpensesRepository repoExpenses
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoExpenses = repoExpenses;
      
    }

    public async Task<bool> Handle(SaveProgramExpensesCommand request, CancellationToken cancellationToken)
    {

        var reqExpenses = mapper.Map<SaveProgramExpensesCommand, FloodProgramExpensesEntity>(request);
        await repoExpenses.SaveExpensesAsync(reqExpenses);
        return true;
    }
}
