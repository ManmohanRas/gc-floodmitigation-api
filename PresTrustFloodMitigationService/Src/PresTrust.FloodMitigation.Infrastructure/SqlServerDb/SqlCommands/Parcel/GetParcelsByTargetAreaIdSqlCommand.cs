namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelsByTargetAreaIdSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT   *,
                        P.[OwnersName] AS [LandOwner],
						FAP.[StatusId]
            FROM	[Flood].[FloodParcel] P
		    LEFT JOIN [Flood].[FloodApplicationParcel] FAP ON (P.PamsPin = FAP.PamsPin)
            WHERE	P.[TargetAreaId] = @p_TargetAreaId;";

    public GetParcelsByTargetAreaIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
