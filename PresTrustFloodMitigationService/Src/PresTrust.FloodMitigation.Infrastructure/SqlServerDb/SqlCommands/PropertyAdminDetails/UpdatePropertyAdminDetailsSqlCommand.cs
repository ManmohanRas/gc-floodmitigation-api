namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdatePropertyAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
 @" UPDATE [Flood].[FloodParcelAdminDetails]
               SET  ApplicationId = @p_ApplicationId
                    ,PamsPin = @p_PamsPin 
                    ,DobDocumentsMissingDate = @p_DobDocumentsMissingDate 
                    ,FmcFinalApprovalDate = @p_FmcFinalApprovalDate 
                    ,FmcFinalNumber = @p_FmcFinalNumber 
                    ,BccFinalNumber = @p_BccFinalNumber 
                    ,BccFinalApprovalDate = @p_BccFinalApprovalDate 
                    ,MunicipalPurchaseDate = @p_MunicipalPurchaseDate 
                    ,MunicipalPurchaseNumber = @p_MunicipalPurchaseNumber 
                    ,GrantAgreementDate = @P_GrantAgreementDate 
                    ,GrantAgreementExpirationDate = @p_GrantAgreementExpirationDate 
                    ,DueDiligenceDocumentsMissingDate = @p_DueDiligenceDocumentsMissingDate 
                    ,ScheduleClosingDate = @p_ScheduleClosingDate 
                    ,SoftCostReimbursementRequestDate = @p_SoftCostReimbursementRequestDate 
                    ,FmcSoftcostReimbApprovalDate = @p_FmcSoftcostReimbApprovalDate 
                    ,FmcSoftcostReimbApprovalNumber = @P_FmcSoftcostReimbApprovalNumber 
                    ,BccSoftcostReimbApprovalDate = @p_BccSoftcostReimbApprovalDate 
                    ,BccSoftcostReimbApprovalNumber = @p_BccSoftcostReimbApprovalNumber 
                    ,DoesHomeOwnerHaveNFIPInsurance = @p_DoesHomeOwnerHaveNFIPInsurance 
                    ,IsDEPInvolved = @p_IsDEPInvolved 
                    ,IsPARRequestedbyFunder = @p_IsPARRequestedbyFunder 
                    ,LastUpdatedBy = @p_LastUpdatedBy
                    ,LastUpdatedOn = @p_LastUpdatedOn
             WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId";

    public UpdatePropertyAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
