namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetProgramManagerParcelsSqlCommand
{
    private readonly string _sqlCommand =
       @"   DECLARE @v_TotalRows INT;
			SELECT @v_TotalRows = COUNT(Id) FROM [Flood].[FloodParcel];
			WITH ParcelCTE AS
			(
				SELECT
					FP.[Id],
					AG.[AgencyName],
					FP.[PamsPin],
					FP.[Block],
					FP.[Lot],
					FP.[QualificationCode] AS [QCode],
					FP.[Latitude],
					FP.[Longitude],
					FP.[StreetNo],
					FP.[StreetAddress],
					CONCAT(FP.[StreetNo], ' ', FP.[StreetAddress]) AS [PropertyAddress],
					FP.[Acreage],
					FP.[OwnersName] AS [LandOwner],
					FP.[OwnersAddress1],
					FP.[OwnersAddress2],
					FP.[OwnersCity],
					FP.[OwnersState],
					FP.[OwnersZipcode],
					FP.[SquareFootage],
					FP.[YearOfConstruction],
					FP.[TotalAssessedValue],
					FP.[LandValue],
					FP.[ImprovementValue],
					FP.[AnnualTaxes],
					FP.[IsValidPamsPin],
					FP.[TargetAreaId],
					CASE WHEN FP.[TargetAreaId] > 0 THEN 1 ELSE 0 END AS [IsFLAP],
					FP.[DateOfFLAP],
					FP.[IsElevated],
					NULL AS [StartNo],
					NULL AS [EndNo],
					NULL AS [TotalNo]
				FROM [Flood].[FloodParcel] FP
				JOIN [Core].[View_AgencyEntities_FLOOD] AG ON AG.[AgencyId] = FP.[AgencyId]
				WHERE @p_SearchText IS NULL OR TRIM(@p_SearchText) = '' OR
				CONCAT(
					AG.[AgencyName],
					FP.[PamsPin],
					FP.[Block],
					FP.[Lot],
					FP.[QualificationCode],
					FP.[StreetNo],
					FP.[StreetAddress],
					FP.[OwnersName]
				) LIKE @p_SearchText
				ORDER BY [Id]
				OFFSET ((@p_PageNumber - 1) * @p_PageRows) ROWS FETCH NEXT @p_PageRows ROWS ONLY
			)
			SELECT * FROM ParcelCTE
			UNION
			SELECT
				0 AS [Id],
				NULL AS [AgencyName],
				NULL AS [PamsPin],
				NULL AS [Block],
				NULL AS [Lot],
				NULL AS [QCode],
				NULL AS [Latitude],
				NULL AS [Longitude],
				NULL AS [StreetNo],
				NULL AS [StreetAddress],
				NULL AS [PropertyAddress],
				NULL AS [Acreage],
				NULL AS [LandOwner],
				NULL AS [OwnersAddress1],
				NULL AS [OwnersAddress2],
				NULL AS [OwnersCity],
				NULL AS [OwnersState],
				NULL AS [OwnersZipcode],
				NULL AS [SquareFootage],
				NULL AS [YearOfConstruction],
				NULL AS [TotalAssessedValue],
				NULL AS [LandValue],
				NULL AS [ImprovementValue],
				NULL AS [AnnualTaxes],
				NULL AS [IsValidPamsPin],
				NULL AS [TargetAreaId],
				0 AS [IsFLAP],
				NULL AS [DateOfFLAP],
				0 AS [IsElevated],
				((@p_PageNumber - 1) * @p_PageRows) + 1 AS [StartNo],
				((@p_PageNumber - 1) * @p_PageRows) + @p_PageRows AS [EndNo],
				@v_TotalRows AS [TotalNo];";

    public GetProgramManagerParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
