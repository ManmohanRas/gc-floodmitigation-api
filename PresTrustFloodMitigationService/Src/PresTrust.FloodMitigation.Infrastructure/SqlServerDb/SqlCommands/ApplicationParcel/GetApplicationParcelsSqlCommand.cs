namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationParcelsSqlCommand
{
    private readonly string _sqlCommand =
            @"  WITH ApplicationParcelCTE AS (
					SELECT
						AppParcels.ApplicationId,
						AppParcels.PamsPin,
						StatusId,
						IsLocked,
						PP.[Priority],
						CASE WHEN OtherPamsPin IS NULL THEN 0 ELSE 1 END AS AlreadyExists
					FROM
						(SELECT * FROM [Flood].[FloodApplicationParcel] WHERE [ApplicationId] = @p_ApplicationId) AppParcels
					LEFT JOIN
						(SELECT DISTINCT PamsPin AS OtherPamsPin FROM [Flood].[FloodApplicationParcel] WHERE [ApplicationId] != @p_ApplicationId) OtherAppParcels
					ON AppParcels.PamsPin = OtherAppParcels.OtherPamsPin
					LEFT JOIN [Flood].[FloodParcelProperty] PP
					ON (AppParcels.ApplicationId != @p_ApplicationId AND AppParcels.PamsPin = PP.PamsPin)
				)
				SELECT
					AP.[PamsPin],
					AP.[StatusId],
					AP.[IsLocked],
					AP.[AlreadyExists],
					AP.[Priority],
					CONCAT(CP.[StreetNo], ' ', CP.[StreetAddress]) AS [PropertyAddress],
					NULL AS [TargetArea],
					CP.[Block],
					CP.[Lot],
					CP.[QualificationCode] AS [QCode],
					CP.[OwnersName] AS [LandOwner]
				FROM		[ApplicationParcelCTE] AP
				JOIN		[Flood].[FloodParcel] CP ON AP.PamsPin = CP.PamsPin;";

    public GetApplicationParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
