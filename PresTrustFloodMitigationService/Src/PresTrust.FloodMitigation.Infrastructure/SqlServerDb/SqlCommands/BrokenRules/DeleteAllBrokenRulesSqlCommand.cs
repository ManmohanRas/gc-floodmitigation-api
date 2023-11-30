namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteAllBrokenRulesSqlCommand
{
    private readonly string _sqlCommand =

		  @" DELETE FROM [Flood].[FloodApplicationBrokenRules]
			 WHERE	ApplicationId = @p_ApplicationId;";
    public DeleteAllBrokenRulesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
