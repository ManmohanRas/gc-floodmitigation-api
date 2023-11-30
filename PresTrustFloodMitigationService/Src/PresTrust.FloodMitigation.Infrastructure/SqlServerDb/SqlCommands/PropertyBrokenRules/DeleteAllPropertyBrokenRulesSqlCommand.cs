namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class DeleteAllPropertyBrokenRulesSqlCommand
    {
        private readonly string _sqlCommand =

         @" DELETE FROM [Flood].[FloodParcelBrokenRules]
			WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin;";
        public DeleteAllPropertyBrokenRulesSqlCommand() { }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
