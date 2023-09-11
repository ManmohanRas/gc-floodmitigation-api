namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetBrokenRulesSqlCommand
{
    private readonly string _sqlCommand =
          @"   SELECT		 [ApplicationId]
							,[SectionId]
							,[Message]
							,[IsApplicantFlow]
				FROM		 [Flood].[FloodApplicationBrokenRules]
				WHERE		 ApplicationId = @p_ApplicationId
				ORDER BY	 SectionId ASC;";

    public GetBrokenRulesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
