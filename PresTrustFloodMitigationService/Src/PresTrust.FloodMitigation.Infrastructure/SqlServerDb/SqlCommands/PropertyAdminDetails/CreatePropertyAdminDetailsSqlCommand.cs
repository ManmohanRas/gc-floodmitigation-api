namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreatePropertyAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
                @"INSERT INTO [Flood].[FloodParcelAdminDetails]
						(
							 ApplicationId
							,PamsPin
							,DobDocumentsMissingDate
							,FmcFinalApprovalDate
							,FmcFinalNumber
							,BccFinalNumber
							,BccFinalApprovalDate
							,MunicipalPurchaseDate
							,MunicipalPurchaseNumber
							,GrantAgreementDate
							,GrantAgreementExpirationDate
							,DueDiligenceDocumentsMissingDate
							,ScheduleClosingDate
							,SoftCostReimbursementRequestDate
							,FmcSoftcostReimbApprovalDate
							,FmcSoftcostReimbApprovalNumber
							,BccSoftcostReimbApprovalDate
							,BccSoftcostReimbApprovalNumber
							,DoesHomeOwnerHaveNFIPInsurance
							,IsDEPInvolved
							,IsPARRequestedbyFunder
							,LastUpdatedBy  
							,LastUpdatedOn	
						)

						VALUES
						(
							 @p_ApplicationId
							,@p_PamsPin
							,@p_DobDocumentsMissingDate
							,@p_FmcFinalApprovalDate
							,@p_FmcFinalNumber
							,@p_BccFinalNumber
							,@p_BccFinalApprovalDate
							,@p_MunicipalPurchaseDate
							,@p_MunicipalPurchaseNumber
							,@P_GrantAgreementDate
							,@p_GrantAgreementExpirationDate
							,@p_DueDiligenceDocumentsMissingDate
							,@p_ScheduleClosingDate
							,@p_SoftCostReimbursementRequestDate
							,@p_FmcSoftcostReimbApprovalDate
							,@P_FmcSoftcostReimbApprovalNumber
							,@p_BccSoftcostReimbApprovalDate
							,@p_BccSoftcostReimbApprovalNumber
							,@p_DoesHomeOwnerHaveNFIPInsurance
							,@p_IsDEPInvolved
							,@p_IsPARRequestedbyFunder
							,@p_LastUpdatedBy  
							,GETDATE()	
						);

				  SELECT CAST( SCOPE_IDENTITY() AS INT);";


    public CreatePropertyAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
