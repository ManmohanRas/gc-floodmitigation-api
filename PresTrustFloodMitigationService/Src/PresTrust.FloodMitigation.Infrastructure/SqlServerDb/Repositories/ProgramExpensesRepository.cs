namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ProgramExpensesRepository : IProgramExpensesRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ProgramExpensesRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodProgramExpensesEntity>> GetAllProgramExpensesAsync()
    {
        List<FloodProgramExpensesEntity> results;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetProgramExpensesSqlCommand();
        results = (await conn.QueryAsync<FloodProgramExpensesEntity>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds
                            )).ToList();
        return results ?? new();
    }

    public async Task<FloodProgramExpensesEntity> SaveExpensesAsync(FloodProgramExpensesEntity expenses)
    {
        if (expenses.Id > 0)
            return await UpdateAsync(expenses);
        else
            return await CreateAsync(expenses);
    }

    private async Task<FloodProgramExpensesEntity> UpdateAsync(FloodProgramExpensesEntity expenses)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateProgramExpensesSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = expenses.Id,
                @p_ExpenseYear = expenses.ExpenseYear,
                @p_ExpenseAmount = expenses.ExpenseAmount,
                @p_ExpenseDate = expenses.ExpenseDate,
                @p_Category = expenses.Category,
                @p_Comment = expenses.Comment,
                @p_LastUpdatedBy = expenses.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return expenses;
    }

    private async Task<FloodProgramExpensesEntity> CreateAsync(FloodProgramExpensesEntity expenses)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateProgramExpensesSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = expenses.Id,
                @p_ExpenseYear = expenses.ExpenseYear,
                @p_ExpenseAmount = expenses.ExpenseAmount,
                @p_ExpenseDate = expenses.ExpenseDate,
                @p_Category = expenses.Category,
                @p_Comment = expenses.Comment,
                @p_LastUpdatedBy = expenses.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        expenses.Id = id;

        return expenses;
    }

    public async Task DeleteProgramExpensesAsync(FloodProgramExpensesEntity expenses)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteProgramExpensesSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = expenses.Id,
                @p_ExpenseYear = expenses.ExpenseYear
            });
    }
}
