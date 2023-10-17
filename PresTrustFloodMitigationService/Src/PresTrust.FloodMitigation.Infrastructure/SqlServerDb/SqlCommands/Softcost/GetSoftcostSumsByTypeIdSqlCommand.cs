namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetSoftcostSumsByTypeIdSqlCommand
{
    private readonly string _sqlCommand =
        @" SELECT  
           (SELECT Title FROM [Flood].[FloodParcelSoftCostType] WHERE Id = SoftCostTypeId) AS Title ,SUM(PaymentAmount) AS Amount 
           FROM [Flood].[FloodParcelSoftCost]
           WHERE [ApplicationId] = @p_ApplicationId
           GROUP BY SoftCostTypeId;";

    public GetSoftcostSumsByTypeIdSqlCommand() { }



    public override string ToString()
    {
        return _sqlCommand;
    }
}
