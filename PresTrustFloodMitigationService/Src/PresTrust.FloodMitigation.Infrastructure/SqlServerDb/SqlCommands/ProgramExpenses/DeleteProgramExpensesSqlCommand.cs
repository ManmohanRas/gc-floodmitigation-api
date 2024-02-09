namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteProgramExpensesSqlCommand
{
    private readonly string _sqlCommand =
       @" DELETE 
              FROM [Flood].[FloodProgramExpenses]
              WHERE Id = @p_Id;";

    public DeleteProgramExpensesSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }

}
