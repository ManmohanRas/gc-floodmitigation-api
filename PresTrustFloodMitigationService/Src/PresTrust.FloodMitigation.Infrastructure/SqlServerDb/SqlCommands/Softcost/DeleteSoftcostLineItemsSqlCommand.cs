namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class DeleteSoftcostLineItemsSqlCommand
{
    private readonly string _sqlCommand =
        @" DELETE 
            FROM [Flood].[FloodParcelSoftCost]
            WHERE Id=@p_Id;";

    public DeleteSoftcostLineItemsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
