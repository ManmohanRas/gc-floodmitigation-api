namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationParcelsSqlCommand
{
    private readonly string _sqlCommand =
            @"  WITH ApplicationParcelCTE AS
				(
					SELECT
						*
					FROM
					(
						SELECT DISTINCT
							AP.[ApplicationId],
							AP.[PamsPin],
							AP.[StatusId],
							AP.[IsLocked],
							PP.[Priority],
							PP.[ValueEstimate]
						FROM [Flood].[FloodApplicationParcel] AP
						LEFT JOIN [Flood].[FloodParcelProperty] PP ON AP.[ApplicationId] = PP.[ApplicationId] AND AP.[PamsPin] = PP.[PamsPin]
						WHERE AP.[ApplicationId] = @p_ApplicationId
					) ApplicationParcels
					LEFT JOIN 
					(
						SELECT DISTINCT
							[PamsPin] AS [OtherPamsPin]
						FROM [Flood].[FloodApplicationParcel]
						WHERE [ApplicationId] != @p_ApplicationId
					) OtherParcels ON ApplicationParcels.[PamsPin] = OtherParcels.[OtherPamsPin]
				)
				SELECT DISTINCT
					CP.[Id],
					AP.[PamsPin],
					AP.[StatusId],
					AP.[IsLocked],
					CASE WHEN AP.[OtherPamsPin] IS NULL THEN 0 ELSE 1 END AS [AlreadyExists],
					AP.[Priority],
					AP.[ValueEstimate],
					CONCAT(CP.[StreetNo], ' ', CP.[StreetAddress]) AS [PropertyAddress],
					NULL AS [TargetArea],
					CP.[Block],
					CP.[Lot],
					CP.[QualificationCode] AS [QCode],
					CP.[OwnersName] AS [LandOwner],
					CP.[IsValidPamsPin]
				FROM [ApplicationParcelCTE] AP
				JOIN [Flood].[FloodParcel] CP ON AP.[PamsPin] = CP.[PamsPin];";

    public GetApplicationParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
