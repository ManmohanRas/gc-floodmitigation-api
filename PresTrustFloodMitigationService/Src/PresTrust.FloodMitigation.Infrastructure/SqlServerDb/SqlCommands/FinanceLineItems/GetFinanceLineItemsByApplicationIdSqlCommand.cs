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
                SELECT CP.PropertyLocation
               ,CTE.[Id]
               ,CTE.[ApplicationId]
               ,CTE.[PamsPin]
               ,CTE.[Priority]
               ,CTE.[ValueEstimate]
                FROM 
                FloodApplicationParcelCTE CTE
                JOIN Core.Parcels CP ON (CTE.PamsPin = cp.PAMS_PIN);";
    public GetFinanceLineItemsByApplicationIdSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
