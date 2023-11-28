namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationPaymentsSqlCommand
{
    private readonly string _sqlCommand =
                    @"    SELECT DISTINCT
                          ISNULL(PP.[Id], 0) AS [Id],
                         AP.[ApplicationId],
                          AP.[PamsPin],
                          CONCAT(P.StreetNo, ' ' , P.StreetAddress) AS [Property],
                          CASE 
                            WHEN AP.[StatusId] IN(1,2,3)
                            THEN  0
                            ELSE PF.[HardCostFMPAmt]
                             END AS [HardCostFMPAmt],
                         CASE 
                           WHEN AP.[StatusId] IN(1,2,3,4)  THEN  0
					       WHEN AP.[IsApproved] = 0 THEN  0
                           ELSE PF.[SoftCostFMPAmt]
                           END AS [SoftCostFMPAmt],
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
