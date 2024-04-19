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
					FP.[AgencyId],
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
					FP.[DateOfFLAP],
					ISNULL(TA.[TargetArea], '') AS [TargetArea],
					CASE WHEN FP.[TargetAreaId] > 0 THEN 1 ELSE 0 END AS [IsFLAP],
					FP.[IsElevated],
					NULL AS [StartNo],
					NULL AS [EndNo],
					NULL AS [TotalNo]
				FROM [Flood].[FloodParcel] FP
				JOIN [Core].[View_AgencyEntities_FLOOD] AG ON AG.[AgencyId] = FP.[AgencyId]
				LEFT JOIN [Flood].[FloodFlapTargetArea] TA ON FP.[TargetAreaId] = TA.[Id]
				WHERE (@p_Block = '' OR FP.[Block] like @p_Block) AND (@p_Lot = '' OR FP.[Lot] like @p_Lot) AND (@p_Address = ''  OR CONCAT(FP.[StreetNo], ' ', FP.[StreetAddress]) like @p_Address) AND (@p_AgencyId = 0 OR FP.[AgencyId] = @p_AgencyId)
				ORDER BY [Id]
				OFFSET ((@p_PageNumber - 1) * @p_PageRows) ROWS FETCH NEXT @p_PageRows ROWS ONLY
			)
			SELECT * FROM ParcelCTE
			UNION
			SELECT
				0 AS [Id],
				0 AS [AgencyId],
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
				NULL AS [DateOfFLAP],
				NULL AS [TargetArea],
				0 AS [IsFLAP],
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
