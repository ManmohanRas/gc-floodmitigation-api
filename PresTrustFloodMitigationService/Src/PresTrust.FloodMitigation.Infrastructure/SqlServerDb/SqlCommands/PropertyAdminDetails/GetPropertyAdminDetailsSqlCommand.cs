namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropertyAdminDetailsSqlCommand
{
    private readonly string _sqlCommand =
     @" SELECT
			PAD.[Id],
			PAD.[ApplicationId],
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
		FROM
		(
			SELECT
				[ApplicationId],
				[MunicipalResolutionDate],
				[MunicipalResolutionNumber],
				[FMCPreliminaryApprovalDate],
				[FMCPreliminaryNumber],
				[BCCPreliminaryApprovalDate],
				[BCCPreliminaryNumber],
				[FundingExpirationDate],
				[FirstFundingExpirationDate],
				[SecondFundingExpirationDate]
			FROM [Flood].[FloodApplicationAdminDetails]
			WHERE ApplicationId = @p_ApplicationId
		) AAD
		LEFT JOIN
		(
			SELECT
				[Id],
				[ApplicationId],
				[PamsPin],
				[BCCFinalApprovalDate],
				[BCCFinalNumber],
				[FMCFinalApprovalDate],
				[FMCFinalNumber],
				[MunicipalPurchaseDate],
				[MunicipalPurchaseNumber],
				[GrantAgreementDate],
				[GrantAgreementExpirationDate],
				[DueDiligenceDocumentsMissingDate],
				[ScheduleClosingDate],
				[SoftCostReimbursementRequestDate],
				[FMCSoftCostReimbApprovalDate],
				[FMCSoftCostReimbApprovalNumber],
				[BCCSoftCostReimbApprovalDate],
				[BCCSoftCostReimbApprovalNumber],
				[DoesHomeOwnerHaveNFIPInsurance],
				[IsDEPInvolved],
				[IsPARRequestedbyFunder],
				[DOBDocumentsMissingDate]
			FROM [Flood].[FloodParcelAdminDetails]
			WHERE ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin
		) PAD ON PAD.ApplicationId = AAD.ApplicationId;";


    public GetPropertyAdminDetailsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
