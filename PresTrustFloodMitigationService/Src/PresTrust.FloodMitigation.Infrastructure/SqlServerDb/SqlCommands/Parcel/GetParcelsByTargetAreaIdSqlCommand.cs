namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelsByTargetAreaIdSqlCommand
{
    private readonly string _sqlCommand =
            @" SELECT DISTINCT   P.[PamsPin],
						P.[AgencyId],
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
						ISNULL(FAP.[StatusId], 0) AS StatusId
            FROM	[Flood].[FloodParcel] P
		    LEFT JOIN [Flood].[FloodApplicationParcel] FAP ON (P.PamsPin = FAP.PamsPin AND FAP.StatusId = 5)
            WHERE	P.[TargetAreaId] = @p_TargetAreaId;";

    public GetParcelsByTargetAreaIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
