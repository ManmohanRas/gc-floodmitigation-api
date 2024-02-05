namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteProgramExpensesSqlCommand
{
    private readonly string _sqlCommand =
       @" DELETE 
              FROM [Flood].[FloodProgramExpenses]
              WHERE ExpenseId = @p_ExpenseId and ExpenseYear = @p_ExpenseYear;";

    public DeleteProgramExpensesSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
