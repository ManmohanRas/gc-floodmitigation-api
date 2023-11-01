namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateApplicationAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
                 @"INSERT INTO [Flood].[FloodApplicationAdminDetails]
						(
							 ApplicationId
							,MunicipalResolutionDate
							,MunicipalResolutionNumber
							,FMCPreliminaryApprovalDate
							,FMCPreliminaryNumber
							,BCCPreliminaryApprovalDate
							,BCCPreliminaryNumber
							,ProjectDescription
							,FundingExpirationDate
							,FirstFundingExpirationDate
							,SecondFundingExpirationDate
							,CommissionerMeetingDate
							,FirstCommitteeMeetingDate
							,SecondCommitteeMeetingDate
							,LastUpdatedBy  
							,LastUpdatedOn	
						)

						VALUES
						(
							 @p_ApplicationId
							,@p_MunicipalResolutionDate
							,@p_MunicipalResolutionNumber
							,@p_FMCPreliminaryApprovalDate
							,@p_FMCPreliminaryNumber
							,@p_BCCPreliminaryApprovalDate
							,@p_BCCPreliminaryNumber
							,@p_ProjectDescription
							,@P_FundingExpirationDate
							,@p_FirstFundingExpirationDate
							,@p_SecondFundingExpirationDate
							,@p_CommissionerMeetingDate
							,@p_FirstCommitteeMeetingDate
							,@p_SecondCommitteeMeetingDate
							,@p_LastUpdatedBy  
							,GETDATE()	
						);

				  SELECT CAST( SCOPE_IDENTITY() AS INT);";


    public CreateApplicationAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
