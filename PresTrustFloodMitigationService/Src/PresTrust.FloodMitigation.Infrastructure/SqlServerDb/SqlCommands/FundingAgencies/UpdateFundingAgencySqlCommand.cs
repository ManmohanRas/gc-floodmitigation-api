namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateFundingAgencySqlCommand
{
    private readonly string _sqlCommand =
       @" UPDATE		       [Flood].[FloodFundingAgency]
             SET			   [FundingAgencyName] = @p_FundingAgencyName
			                  ,[CurrentStatus] = @p_CurrentStatus
			                  ,[DateOfApproval] = @p_DateOfApproval
             WHERE		       Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateFundingAgencySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
