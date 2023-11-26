namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationPaymentsSqlCommand
{
    private readonly string _sqlCommand =
                    @"    SELECT DISTINCT
                          ISNULL(PP.[Id], 0) AS [Id],
                         AP.[ApplicationId],
                          AP.[PamsPin],
                          CONCAT(P.StreetNo, ' ' , P.StreetAddress) AS [Property],
                          PF.[HardCostFMPAmt],
                          PF.[SoftCostFMPAmt],
                          PP.[HardCostPaymentTypeId],
                          PP.[HardCostPaymentDate],
                          PP.[HardCostPaymentStatusId],
                          PP.[SoftCostPaymentTypeId],
                          PP.[SoftCostPaymentDate],
                          PP.[SoftCostPaymentStatusId],
                          PR.[Priority],
                         ISNULL(PF.[EstimatePurchasePrice], PR.[ValueEstimate]) AS EstimatePurchasePrice,
						 PF.[AdditionalSoftCostEstimate],
						 AF.[MatchPercent]
                    FROM [Flood].[FloodApplicationParcel] AP
                    LEFT JOIN [Flood].[FloodParcel] P
                          ON (AP.[PamsPin] = P.[PamsPin])
                    LEFT JOIN [Flood].[FloodParcelFinance] PF
                          ON (AP.[ApplicationId] = PF.[ApplicationId] AND AP.[PamsPin] = PF.[PamsPin])
                    LEFT JOIN [Flood].[FloodParcelPayment] PP
                          ON (AP.[ApplicationId] = PP.[ApplicationId] AND AP.[PamsPin] = PP.[PamsPin])
                    LEFT JOIN [Flood].[FloodParcelProperty] PR
                          ON (AP.[ApplicationId] = PR.[ApplicationId] AND AP.[PamsPin] = PR.[PamsPin])
                    LEFT JOIN [Flood].[FloodApplicationFinance] AF 
				         ON (AP.ApplicationId = AF.ApplicationId)
                    WHERE AP.[ApplicationId] = @p_ApplicationId AND PR.[Priority] = 1;";

    public GetApplicationPaymentsSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
