namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFinanceLineItemsByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
             @"  WITH FloodApplicationParcelCTE AS (
                 SELECT FLI.[Id]
                       ,FAP.[ApplicationId]
                       ,FAP.[PamsPin]
                       ,FLI.[Priority]
                       ,FLI.[ValueEstimate]
                 FROM [Flood].[FloodApplicationParcel] FAP 
                 LEFT JOIN [Flood].[FloodApplicationFinanceLineItems] FLI ON (FAP.ApplicationId = FLI.ApplicationId AND FAP.PamsPin = FLI.PamsPin) 
                 WHERE FAP.ApplicationId = @p_applicationId) 
                SELECT CONCAT (FP.StreetNo, ' ',  FP.StreetAddress) AS PropertyLocation
               ,CTE.[Id]
               ,CTE.[ApplicationId]
               ,CTE.[PamsPin]
               ,CTE.[Priority]
               ,CTE.[ValueEstimate]
                FROM 
                FloodApplicationParcelCTE CTE
                JOIN [Flood].[FloodParcel] FP ON (CTE.PamsPin = FP.PAMS_PIN);";
    public GetFinanceLineItemsByApplicationIdSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
