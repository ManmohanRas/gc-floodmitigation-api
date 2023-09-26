namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationFundingAgencySqlCommand
{
    private readonly string _sqlCommand =
       @"INSERT INTO [Flood].[FloodApplicationFundingAgency]
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

    public CreateApplicationFundingAgencySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
