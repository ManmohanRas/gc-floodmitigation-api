namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteBrokenRulesSqlCommand
{
    private readonly string _sqlCommand =

		  @" DELETE 
			 FROM [Flood].[FloodApplicationBrokenRules]
			 WHERE	ApplicationId = @p_ApplicationId AND SectionId = @p_SectionId;";
    public DeleteBrokenRulesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
