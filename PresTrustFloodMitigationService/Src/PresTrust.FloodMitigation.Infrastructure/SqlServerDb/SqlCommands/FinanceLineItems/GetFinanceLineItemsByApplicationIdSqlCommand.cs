namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFinanceLineItemsByApplicationIdSqlCommand
{
    private readonly string _sqlCommand =
             @"  SELECT   * FROM  [Flood].[FloodParcel] FP
                            LEFT JOIN [Flood].[FloodApplicationParcel] FAP ON (FP.PamsPin = FAP.PamsPin);";
    public GetFinanceLineItemsByApplicationIdSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
