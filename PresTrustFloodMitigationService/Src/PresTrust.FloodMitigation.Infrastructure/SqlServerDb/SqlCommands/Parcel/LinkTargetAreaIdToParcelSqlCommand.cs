namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class LinkTargetAreaIdToParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[TargetAreaId] = @p_TargetAreaId,
					[DateOfFlap]  = @p_DateOfFlap
				WHERE [PamsPin] = @p_PamsPin";

    public LinkTargetAreaIdToParcelSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
