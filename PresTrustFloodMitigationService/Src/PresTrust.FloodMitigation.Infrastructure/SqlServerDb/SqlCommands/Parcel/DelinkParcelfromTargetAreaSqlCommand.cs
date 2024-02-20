namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

    public class DelinkParcelfromTargetAreaSqlCommand
{
    private readonly string _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[TargetAreaId] = null,
					[DateOfFlap]  = null
				WHERE [PamsPin] = @p_PamsPin";

    public DelinkParcelfromTargetAreaSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}

