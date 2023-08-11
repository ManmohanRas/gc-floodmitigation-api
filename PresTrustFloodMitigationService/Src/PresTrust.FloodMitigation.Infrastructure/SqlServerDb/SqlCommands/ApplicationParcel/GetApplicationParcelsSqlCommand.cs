namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationParcelsSqlCommand
{
    private readonly string _sqlCommand =
            @"  WITH ApplicationParcelCTE AS (
					SELECT
						ApplicationId,
						PamsPin,
						IsLocked,
						CASE WHEN OtherPamsPin IS NULL THEN 0 ELSE 1 END AS AlreadyExists
					FROM
						(SELECT * FROM [Flood].[FloodApplicationParcel] WHERE [ApplicationId] = @p_ApplicationId) AppParcels
					LEFT JOIN
						(SELECT DISTINCT PamsPin AS OtherPamsPin FROM [Flood].[FloodApplicationParcel] WHERE [ApplicationId] != @p_ApplicationId) OtherAppParcels
					ON AppParcels.PamsPin = OtherAppParcels.OtherPamsPin
				)
				SELECT
					AP.[PamsPin],
					AP.[IsLocked],
					AP.[AlreadyExists],
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
