namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteSoftCostLineItemsSqlCommand
{
    private readonly string _sqlCommand =
        @" DELETE 
            FROM [Flood].[FloodParcelSoftCost]
            WHERE Id=@p_Id;";

    public DeleteSoftCostLineItemsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
