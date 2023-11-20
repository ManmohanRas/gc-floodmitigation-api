namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class DeletePropertyBrokenRulesSqlCommand
    {
        private readonly string _sqlCommand =

         @" DELETE 
			 FROM [Flood].[FloodParcelBrokenRules]
			 WHERE	ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin AND SectionId = @p_SectionId;";
        public DeletePropertyBrokenRulesSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
