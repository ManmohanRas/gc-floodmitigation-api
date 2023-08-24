namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateFundingAgencySqlCommand
{
    private readonly string _sqlCommand =
       @"INSERT INTO [Flood].[FloodFundingAgency]
                   ([ApplicationId]
                   ,[FundingAgencyName]
                   ,[CurrentStatus]
                   ,[DateOfApproval])
             VALUES
                   (@p_ApplicationId
                   ,@p_FundingAgencyName
                   ,@p_CurrentStatus
                   ,@p_DateOfApproval);

           SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateFundingAgencySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
