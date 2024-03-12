namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelsByTargetAreaIdSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT DISTINCT 
						P.[Id],
						P.[PamsPin],
						P.[AgencyId],
					    AG.[AgencyName],
						P.[Block],
						P.[Lot],
						P.[QualificationCode] AS [QCode],
						P.[Latitude],
						P.[Longitude],
						P.[StreetNo],
						P.[StreetAddress],
						P.[Acreage],
						P.[OwnersName] AS [LandOwner],
						P.[OwnersAddress1],
						P.[OwnersAddress2],
						P.[OwnersCity],
						P.[OwnersState],
						P.[OwnersZipcode],
						P.[SquareFootage],
						P.[YearOfConstruction],
						P.[TotalAssessedValue],
						P.[LandValue],
						P.[ImprovementValue],
						P.[AnnualTaxes],
						P.[DateOfFLAP],
						P.[IsValidPamsPin],
						ISNULL(FAP.[StatusId], 0) AS StatusId,
						ISNULL(TA.[TargetArea], '') AS [TargetArea],
						P.[IsElevated],
					CASE WHEN P.[TargetAreaId] > 0 THEN 1 ELSE 0 END AS [IsFLAP]
            FROM	[Flood].[FloodParcel] P
			JOIN [Core].[View_AgencyEntities_FLOOD] AG ON AG.[AgencyId] = P.[AgencyId]
			LEFT JOIN [Flood].[FloodFlapTargetArea] TA ON P.[TargetAreaId] = TA.[Id]
		    LEFT JOIN [Flood].[FloodApplicationParcel] FAP ON (P.PamsPin = FAP.PamsPin AND FAP.StatusId = 5)
            WHERE	P.[TargetAreaId] = @p_TargetAreaId;";

    public GetParcelsByTargetAreaIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
