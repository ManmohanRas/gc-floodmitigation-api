namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelFinanceSqlCommand
{
    private readonly string _sqlCommand =
           @"  SELECT		    PF.[Id]
							   ,PF.[ApplicationId]
                               ,PF.[PamsPin]
                               ,FPP.[ValueEstimate]
                               ,CASE 
									WHEN FPP.[EstimatedPurchasePrice] = 0 THEN FPP.[ValueEstimate]
                                ELSE FPP.[EstimatedPurchasePrice]
                                END AS EstimatePurchasePrice
							   ,FAF.[MatchPercent]
                               ,PF.[AdditionalSoftCostEstimate]
                               ,PF.[AppraisedValue]
                               ,PF.[AMV]
                               ,PF.[TotalFEMABenifits]
                               ,PF.[DOBAffidavitType]
                               ,PF.[DOBAffidavitAmt]
                               ,PF.[HardCostFMPAmt]
                               ,PF.[SoftCostFMPAmt]
                               ,PF.[AppraisersFee]
                               ,PF.[SurveyorsFee]
							   ,PF.[LastUpdatedBy]
							   ,PF.[LastUpdatedOn]
                FROM		  [Flood].[FloodParcelProperty] FPP
				LEFT JOIN     [Flood].[FloodParcelFinance] PF ON (PF.ApplicationId = FPP.ApplicationId AND PF.PamsPin = FPP.PamsPin)
				LEFT JOIN     [Flood].[FloodApplicationFinance] FAF ON (FAF.ApplicationId = FPP.ApplicationId)
				WHERE		  FPP.[ApplicationId] = @p_ApplicationId AND FPP.[PamsPin] = @p_PamsPin";

    public GetParcelFinanceSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
