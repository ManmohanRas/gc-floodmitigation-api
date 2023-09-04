namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteFundingAgencySqlCommand
{
    private readonly string _sqlCommand =
       @"    DELETE		 [Flood].[FloodFundingAgency]
             WHERE		 Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteFundingAgencySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
