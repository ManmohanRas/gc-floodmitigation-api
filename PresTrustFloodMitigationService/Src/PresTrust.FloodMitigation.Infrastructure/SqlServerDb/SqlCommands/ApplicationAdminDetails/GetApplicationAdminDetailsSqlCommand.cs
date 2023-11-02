namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
      @"  SELECT   [Id]
                    ,[ApplicationId]
                    ,[MunicipalResolutionDate]
                    ,[MunicipalResolutionNumber]
                    ,[FMCPreliminaryApprovalDate]
                    ,[FMCPreliminaryNumber]
                    ,[BCCPreliminaryApprovalDate]
                    ,[BCCPreliminaryNumber]
                    ,[ProjectDescription]
                    ,[FundingExpirationDate]
                    ,[FirstFundingExpirationDate]
                    ,[SecondFundingExpirationDate]
                    ,[CommissionerMeetingDate]
                    ,[FirstCommitteeMeetingDate]
                    ,[SecondCommitteeMeetingDate]
                    ,[LastUpdatedBy]
                    ,[LastUpdatedOn]
            FROM [Flood].[FloodApplicationAdminDetails]
            WHERE ApplicationId = @p_ApplicationId";


    public GetApplicationAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
