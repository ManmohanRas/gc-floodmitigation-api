namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class LinkTargetAreaIdToParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[TargetAreaId] = @p_TargetAreaId,
					[DateOfFlap]  = @p_DateOfFlap
				WHERE [Id] = @p_Id";

    public LinkTargetAreaIdToParcelSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
