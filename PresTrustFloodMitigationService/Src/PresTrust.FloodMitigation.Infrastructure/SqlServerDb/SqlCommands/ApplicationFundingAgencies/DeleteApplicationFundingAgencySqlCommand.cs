namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteApplicationFundingAgencySqlCommand
{
    private readonly string _sqlCommand =
       @"    DELETE		 [Flood].[FloodApplicationFundingAgency]
             WHERE		 Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteApplicationFundingAgencySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
