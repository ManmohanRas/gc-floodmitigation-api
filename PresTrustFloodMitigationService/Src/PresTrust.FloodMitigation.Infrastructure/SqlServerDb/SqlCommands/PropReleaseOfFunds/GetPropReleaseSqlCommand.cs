namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropReleaseSqlCommand
{
    private readonly string _sqlCommand =
         @" SELECT DISTINCT
                  ISNULL(PP.[Id], 0) AS [Id],
                  PY.[CAFNumber],
                 CASE 
                       WHEN AP.[StatusId] IN(4,5,6)
                       THEN  PF.[HardCostFMPAmt]
                       ELSE  0
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
                  PP.[SoftCostPaymentStatusId]
            FROM [Flood].[FloodApplicationParcel] AP
            LEFT JOIN [Flood].[FloodApplicationPayment] PY
                  ON AP.[ApplicationId] = PY.[ApplicationId]
            LEFT JOIN [Flood].[FloodParcelFinance] PF
                  ON AP.[ApplicationId] = PF.[ApplicationId] AND AP.[PamsPin] = PF.[PamsPin]
            LEFT JOIN [Flood].[FloodParcelPayment] PP
                  ON AP.[ApplicationId] = PP.[ApplicationId] AND AP.[PamsPin] = PP.[PamsPin]
            WHERE AP.[ApplicationId] = @p_ApplicationId AND AP.[PamsPin] = @p_PamsPin;";

    public GetPropReleaseSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }

}