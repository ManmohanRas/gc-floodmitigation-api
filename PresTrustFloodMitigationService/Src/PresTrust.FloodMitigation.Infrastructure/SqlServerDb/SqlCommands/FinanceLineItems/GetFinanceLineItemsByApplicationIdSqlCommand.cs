namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFinanceLineItemsByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
             @"  WITH FloodApplicationParcelCTE AS (
                 SELECT FLI.[Id]
                       ,FAP.[ApplicationId]
                       ,FAP.[PamsPin]
                       ,ISNULL(FPP.[Priority], 1) AS Priority
                       ,FPP.[ValueEstimate]
                 FROM [Flood].[FloodApplicationParcel] FAP 
                 LEFT JOIN [Flood].[FloodApplicationFinanceLineItems] FLI ON (FAP.ApplicationId = FLI.ApplicationId AND FAP.PamsPin = FLI.PamsPin) 
                 LEFT JOIN [Flood].[FloodParcelProperty] FPP ON (FAP.ApplicationId = FPP.ApplicationId AND FAP.PamsPin = FPP.PamsPin) 
                 WHERE FAP.ApplicationId = @p_applicationId) 
                SELECT CONCAT (FP.StreetNo, ' ',  FP.StreetAddress) AS PropertyLocation
               ,CTE.[Id]
               ,CTE.[ApplicationId]
               ,CTE.[PamsPin]
               ,CTE.[Priority]
               ,CTE.[ValueEstimate]
                FROM 
                FloodApplicationParcelCTE CTE
                JOIN [Flood].[FloodParcel] FP ON (CTE.PamsPin = FP.PamsPin);";
    public GetFinanceLineItemsByApplicationIdSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
