namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelsByTargetAreaIdSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT   P.[Id],
						P.[PamsPin],
						P.[AgencyId],
						P.[TargetAreaId],
						P.[Block],
						P.[Lot],
						P.[QualificationCode] AS [QCode],
						P.[OwnersName] AS [LandOwner],
						P.[StreetNo],
						P.[StreetAddress],
						P.[DateOfFLAP],
						P.[IsElevated],
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
