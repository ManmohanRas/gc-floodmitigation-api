namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationFundingAgencySqlCommand
{
    private readonly string _sqlCommand =
       @" UPDATE		       [Flood].[FloodApplicationFundingAgency]
             SET			   [FundingAgencyName] = @p_FundingAgencyName
			                  ,[CurrentStatus] = @p_CurrentStatus
			                  ,[DateOfApproval] = @p_DateOfApproval
             WHERE		       Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateApplicationFundingAgencySqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
