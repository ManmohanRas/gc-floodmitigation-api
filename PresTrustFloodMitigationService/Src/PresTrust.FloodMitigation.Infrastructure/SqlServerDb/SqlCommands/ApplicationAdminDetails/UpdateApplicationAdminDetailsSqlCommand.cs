namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateApplicationAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
  @" UPDATE [Flood].[FloodApplicationAdminDetails]
               SET  ApplicationId = @p_ApplicationId
                    ,MunicipalResolutionDate = @p_MunicipalResolutionDate 
                    ,MunicipalResolutionNumber = @p_MunicipalResolutionNumber 
                    ,FMCPreliminaryApprovalDate = @p_FMCPreliminaryApprovalDate 
                    ,FMCPreliminaryNumber = @p_FMCPreliminaryNumber 
                    ,BCCPreliminaryApprovalDate = @p_BCCPreliminaryApprovalDate 
                    ,BCCPreliminaryNumber = @p_BCCPreliminaryNumber 
                    ,ProjectDescription = @p_ProjectDescription 
                    ,FundingExpirationDate = @P_FundingExpirationDate
                    ,FirstFundingExpirationDate = @p_FirstFundingExpirationDate 
                    ,SecondFundingExpirationDate = @p_SecondFundingExpirationDate 
                    ,CommissionerMeetingDate = @p_CommissionerMeetingDate 
                    ,FirstCommitteeMeetingDate = @p_FirstCommitteeMeetingDate 
                    ,SecondCommitteeMeetingDate = @p_SecondCommitteeMeetingDate 
                    ,LastUpdatedBy = @p_LastUpdatedBy
                    ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId";

    public UpdateApplicationAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
