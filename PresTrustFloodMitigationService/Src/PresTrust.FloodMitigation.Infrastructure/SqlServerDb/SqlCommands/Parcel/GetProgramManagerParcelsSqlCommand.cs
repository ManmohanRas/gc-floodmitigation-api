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
					CONCAT(FP.[StreetNo], ' ', FP.[StreetAddress]) AS [PropertyAddress],
					FP.[OwnersName] AS [LandOwner],
					1 AS [IsFLAP],
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
				NULL AS [PropertyAddress],
				NULL AS [LandOwner],
				0 AS [IsFLAP],
				((@p_PageNumber - 1) * @p_PageRows) + 1 AS [StartNo],
				((@p_PageNumber - 1) * @p_PageRows) + @p_PageRows AS [EndNo],
				@v_TotalRows AS [TotalNo];";

    public GetProgramManagerParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
