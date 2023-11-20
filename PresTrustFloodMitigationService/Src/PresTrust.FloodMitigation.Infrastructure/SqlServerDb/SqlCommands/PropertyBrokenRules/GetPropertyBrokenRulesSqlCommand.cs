namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class GetPropertyBrokenRulesSqlCommand
    {
        private readonly string _sqlCommand =
          @"   SELECT		 [ApplicationId]
                            ,[PamsPin]
							,[SectionId]
							,[Message]
							,[IsPropertyFlow]
				FROM		 [Flood].[FloodParcelBrokenRules]
				WHERE		 ApplicationId = @p_ApplicationId AND PamsPin = @p_Pamspin
				ORDER BY	 SectionId ASC;";

        public GetPropertyBrokenRulesSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }

    }
}
