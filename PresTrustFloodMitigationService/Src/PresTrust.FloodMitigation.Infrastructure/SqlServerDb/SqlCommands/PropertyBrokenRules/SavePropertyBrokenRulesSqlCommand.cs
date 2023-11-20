namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb
{ 
    public class SavePropertyBrokenRulesSqlCommand
    {
        private readonly string _sqlCommand =
           @"  INSERT INTO [Flood].[FloodParcelBrokenRules]
				([ApplicationId]
                ,[PamsPin]
				,[SectionId]
				,[Message]
				,[IsPropertyFlow])
			VALUES
				(@p_ApplicationId
                ,@p_PamsPin
				,@p_SectionId 
				,@p_Message
				,@p_IsPropertyFlow);

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

        public SavePropertyBrokenRulesSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
