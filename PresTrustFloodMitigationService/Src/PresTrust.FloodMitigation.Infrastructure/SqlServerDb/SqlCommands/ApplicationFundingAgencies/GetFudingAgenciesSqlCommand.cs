namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFudingAgenciesSqlCommand
{
    private readonly string _sqlCommand =
                @"  SELECT	   [Id]
							  ,[ApplicationId]
							  ,[FundingAgencyName]
                              ,[CurrentStatus]
                              ,[DateOfApproval]
				FROM		   [Flood].[FloodApplicationFundingAgency]
				WHERE		   ApplicationId = @p_ApplicationId;";

    public GetFudingAgenciesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
