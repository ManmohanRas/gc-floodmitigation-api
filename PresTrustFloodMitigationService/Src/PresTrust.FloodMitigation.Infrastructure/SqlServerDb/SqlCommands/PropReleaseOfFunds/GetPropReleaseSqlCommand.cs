namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetPropReleaseSqlCommand
{
    private readonly string _sqlCommand =
         @" SELECT
                  ISNULL(PP.[Id], 0) AS [Id],
                  PY.[CAFNumber],
                 CASE 
                       WHEN AP.[StatusId] < 4 
                       THEN  0
                       ELSE PF.[HardCostFMPAmt]
                        END AS [HardCostFMPAmt],
                 CASE 
                       WHEN AP.[StatusId] < 4 
                       THEN  0
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