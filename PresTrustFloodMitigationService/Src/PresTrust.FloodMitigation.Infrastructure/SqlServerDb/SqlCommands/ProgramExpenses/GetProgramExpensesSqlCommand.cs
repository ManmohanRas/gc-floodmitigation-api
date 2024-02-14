namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetProgramExpensesSqlCommand
{
	private readonly string _sqlCommand =
	 @" SELECT
			[Id],
			[ExpenseYear],
			[ExpenseAmount],
			[ExpenseDate],
			[CategoryId],
			[Comment],
			[LastUpdatedBy],
			[LastUpdatedOn]
		FROM [Flood].[FloodProgramExpenses];";

    public GetProgramExpensesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
