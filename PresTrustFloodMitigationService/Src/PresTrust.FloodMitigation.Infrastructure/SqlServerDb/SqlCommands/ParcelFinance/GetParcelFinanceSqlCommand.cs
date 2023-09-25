namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelFinanceSqlCommand
{
    private readonly string _sqlCommand =
           @"  SELECT		    PF.[Id]
							   ,PF.[ApplicationId]
                               ,PF.[PamsPin]
                               ,FAFLI.[ValueEstimate]
							   ,ISNULL(PF.[EstimatePurchasePrice], FAFLI.[ValueEstimate]) AS EstimatePurchasePrice
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
                FROM		  [Flood].[FloodParcelFinance] PF
				LEFT JOIN     [Flood].[FloodApplicationFinanceLineItems] FAFLI ON (PF.ApplicationId = FAFLI.ApplicationId AND PF.PamsPin = FAFLI.PamsPin)
				LEFT JOIN     [Flood].[FloodApplicationFinance] FAF ON (PF.ApplicationId = FAF.ApplicationId)
				WHERE		  PF.[ApplicationId] = @p_ApplicationId AND PF.[PamsPin] = @p_PamsPin";

    public GetParcelFinanceSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
