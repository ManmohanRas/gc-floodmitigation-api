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
							ISNULL(PSL.[StatusId], 0) AS [PrevStatusId],
							PSL.[LastUpdatedOn],
							AP.[IsLocked],
							PP.[Priority],
							PP.[ValueEstimate]
						FROM [Flood].[FloodApplicationParcel] AP
						LEFT JOIN [Flood].[FloodParcelStatusLog] PSL ON PSL.StatusId != AP.StatusId AND AP.ApplicationId = PSL.ApplicationId AND AP.PamsPin = PSL.PamsPin
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
					AP.[ApplicationId],
					AP.[PamsPin],
					AP.[StatusId],
					AP.[PrevStatusId],
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
				JOIN (SELECT
						ApplicationId,
						PamsPin,
						MAX(LastUpdatedOn) AS LastUpdatedOn
						FROM [ApplicationParcelCTE]
						GROUP BY ApplicationId, PamsPin) prevStatusAP
					ON AP.ApplicationId = prevStatusAP.ApplicationId AND AP.PamsPin = prevStatusAP.PamsPin
						AND AP.LastUpdatedOn = prevStatusAP.LastUpdatedOn
				JOIN [Flood].[FloodParcel] CP ON AP.[PamsPin] = CP.[PamsPin];";

    public GetApplicationParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
