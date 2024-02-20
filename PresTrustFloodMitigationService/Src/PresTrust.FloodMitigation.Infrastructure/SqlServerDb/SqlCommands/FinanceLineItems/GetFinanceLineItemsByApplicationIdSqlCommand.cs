namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFinanceLineItemsByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
             @" WITH FloodApplicationParcelCTE AS (
					SELECT
						FLI.[Id],
						FAP.[ApplicationId],
						FAP.[PamsPin],
						FAP.[IsLocked],
						ISNULL(FPP.[Priority], 1) AS [Priority],
						FPP.[ValueEstimate]
					FROM [Flood].[FloodApplicationParcel] FAP 
					LEFT JOIN [Flood].[FloodApplicationFinanceLineItems] FLI ON (FAP.ApplicationId = FLI.ApplicationId AND FAP.PamsPin = FLI.PamsPin) 
					LEFT JOIN [Flood].[FloodParcelProperty] FPP ON (FAP.ApplicationId = FPP.ApplicationId AND FAP.PamsPin = FPP.PamsPin) 
					WHERE FAP.ApplicationId = @p_ApplicationId
				)
				SELECT
					CASE
						WHEN CTE.[IsLocked] = 1
							THEN CONCAT(LP.StreetNo, ' ' , LP.StreetAddress)
						ELSE CONCAT(FP.StreetNo, ' ' , FP.StreetAddress)
					END AS [PropertyLocation],
					CTE.[Id],
					CTE.[ApplicationId],
					CTE.[PamsPin],
					CTE.[Priority],
					CTE.[ValueEstimate]
				FROM [FloodApplicationParcelCTE] CTE
				LEFT JOIN [Flood].[FloodLockedParcel] LP
						ON (CTE.[IsLocked] = 1 AND CTE.[ApplicationId] = LP.[ApplicationId] AND CTE.[PamsPin] = LP.[PamsPin])
				LEFT JOIN [Flood].[FloodParcel] FP
      					ON (CTE.[IsLocked] = 0 AND CTE.[PamsPin] = FP.[PamsPin]);";
    public GetFinanceLineItemsByApplicationIdSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
