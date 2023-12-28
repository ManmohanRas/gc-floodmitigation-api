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
						P.[Latitude],
						P.[Longitude],
						P.[StreetNo],
						P.[StreetAddress],
						P.[DateOfFLAP]
            FROM	[Flood].[FloodParcel] P
            WHERE	P.[ID] = @p_Id;";

    public GetParcelsByTargetAreaIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
