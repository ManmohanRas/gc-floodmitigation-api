namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropertyAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
     @" SELECT PAD.[ApplicationId],
				PAD.[PamsPin],
				 AAD.[MunicipalResolutionDate],
				 AAD.[MunicipalResolutionNumber],
				 AAD.[FMCPreliminaryApprovalDate],
				 AAD.[FMCPreliminaryNumber],
				 AAD.[BCCPreliminaryApprovalDate],
				 AAD.[BCCPreliminaryNumber],
				 AAD.[FundingExpirationDate],
				 AAD.[FirstFundingExpirationDate],
				 AAD.[SecondFundingExpirationDate],
				 PAD.[BCCFinalApprovalDate],
				 PAD.[BCCFinalNumber],
				 PAD.[FMCFinalApprovalDate],
				 PAD.[FMCFinalNumber],
				 PAD.[MunicipalPurchaseDate],
				 PAD.[MunicipalPurchaseNumber],
				 PAD.[GrantAgreementDate],
				 PAD.[GrantAgreementExpirationDate],
				 PAD.[DueDiligenceDocumentsMissingDate],
				 PAD.[ScheduleClosingDate],
				 PAD.[SoftCostReimbursementRequestDate],
				 PAD.[FMCSoftCostReimbApprovalDate],
				 PAD.[FMCSoftCostReimbApprovalNumber],
				 PAD.[BCCSoftCostReimbApprovalDate],
				 PAD.[BCCSoftCostReimbApprovalNumber],
				 PAD.[DoesHomeOwnerHaveNFIPInsurance],
				 PAD.[IsDEPInvolved],
				 PAD.[IsPARRequestedbyFunder],
				 PAD.[DOBDocumentsMissingDate]
			FROM [Flood].[FloodParcelAdminDetails] PAD
			LEFT JOIN [Flood].[FloodApplicationAdminDetails] AAD ON (PAD.ApplicationId = AAD.ApplicationId)
						WHERE PAD.ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin";


    public GetPropertyAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
