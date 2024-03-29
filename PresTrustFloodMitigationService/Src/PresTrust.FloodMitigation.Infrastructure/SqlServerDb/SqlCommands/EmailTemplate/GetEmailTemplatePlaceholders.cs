namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetEmailTemplatePlaceholders
{
    private readonly string _sqlCommand =
                 @"SELECT AP.ApplicationId,
						   FP.[PamsPin],
	                       CONCAT(FP.[StreetNo], ' ', FP.[StreetAddress]) AS [PropertyAddress],
	                       PAD.[BCCFinalApprovalDate],
	                       PF.[HardCostFMPAmt],
	                       PF.[SoftCostFMPAmt],
                           PAD.[GrantAgreementExpirationDate] AS GrantExpirationDate
                   FROM [Flood].[FloodApplicationParcel] AP
                   LEFT JOIN [Flood].[FloodParcel] FP ON (AP.PamsPin = FP.PamsPin)
				   LEFT JOIN [Flood].[FloodParcelAdminDetails] PAD ON (PAD.ApplicationId = AP.ApplicationId AND PAD.PamsPin = AP.PamsPin)
				   LEFT JOIN [Flood].[FloodParcelFinance] PF ON (PF.ApplicationId = AP.ApplicationId AND PF.PamsPin = AP.PamsPin)
                   WHERE AP.ApplicationId = @p_ApplicationId AND AP.[PamsPin] = @p_PamsPin;";

    public GetEmailTemplatePlaceholders(bool isApplicationFlow)
    {
        if (isApplicationFlow)
        {
            _sqlCommand =
                @"SELECT 
                     AAD.[FundingExpirationDate]
                    ,AAD.[FirstFundingExpirationDate]
                    ,AAD.[SecondFundingExpirationDate]
                FROM [Flood].[FloodApplication] A
                JOIN [Flood].[FloodApplicationAdminDetails] AAD ON (AAD.ApplicationId = A.Id)";
        }else
        {
            _sqlCommand = 
            @"SELECT AP.ApplicationId,
						   FP.[PamsPin],
	                       CONCAT(FP.[StreetNo], ' ', FP.[StreetAddress]) AS [PropertyAddress],
	                       PAD.[BCCFinalApprovalDate],
	                       PF.[HardCostFMPAmt],
	                       PF.[SoftCostFMPAmt],
                           PAD.[GrantAgreementExpirationDate] AS GrantExpirationDate
                   FROM [Flood].[FloodApplicationParcel] AP
                   LEFT JOIN [Flood].[FloodParcel] FP ON (AP.PamsPin = FP.PamsPin)
				   LEFT JOIN [Flood].[FloodParcelAdminDetails] PAD ON (PAD.ApplicationId = AP.ApplicationId AND PAD.PamsPin = AP.PamsPin)
				   LEFT JOIN [Flood].[FloodParcelFinance] PF ON (PF.ApplicationId = AP.ApplicationId AND PF.PamsPin = AP.PamsPin)
                   WHERE AP.ApplicationId = @p_ApplicationId AND AP.[PamsPin] = @p_PamsPin";
        }
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
