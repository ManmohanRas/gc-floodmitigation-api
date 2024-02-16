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
							AP.[IsLocked],
							AP.[IsApproved],
							PP.[Priority],
							PP.[ValueEstimate]
						FROM [Flood].[FloodApplicationParcel] AP
						LEFT JOIN (
							SELECT
								ApplicationId,
								PamsPin,
								StatusId,
								ROW_NUMBER() OVER(PARTITION BY ApplicationId, PamsPin ORDER BY StatusDate DESC) AS LastUpdatedOrder
							FROM [Flood].[FloodParcelStatusLog]
							WHERE ApplicationId = @p_ApplicationId
						) PSL ON PSL.LastUpdatedOrder = 2 AND AP.ApplicationId = PSL.ApplicationId AND AP.PamsPin = PSL.PamsPin
						LEFT JOIN [Flood].[FloodParcelProperty] PP ON AP.[ApplicationId] = PP.[ApplicationId] AND AP.[PamsPin] = PP.[PamsPin]
						WHERE AP.[ApplicationId] = @p_ApplicationId
					) ApplicationParcels
					LEFT JOIN (
						SELECT DISTINCT
							P.[PamsPin] AS [WaitingPamsPin]
						FROM [Flood].[FloodApplicationParcel] P
						LEFT JOIN (
							SELECT
								ApplicationId,
								PamsPin,
								StatusDate,
								ROW_NUMBER() OVER(PARTITION BY ApplicationId, PamsPin ORDER BY StatusDate DESC) AS LastUpdatedOrder
							FROM [Flood].[FloodParcelStatusLog]
						) PSL ON P.[ApplicationId] = PSL.[ApplicationId] AND P.[PamsPin] = PSL.[PamsPin] AND PSL.LastUpdatedOrder = 1
						WHERE P.[ApplicationId] != 16 AND P.[StatusId] IN (6,8,9)
							AND CAST(PSL.[StatusDate] AS DATE) > CAST(DATEADD(DAY, 1, DATEADD(YEAR, -1, SYSDATETIME())) AS DATE)
					) WaitingParcels ON ApplicationParcels.[PamsPin] = WaitingParcels.[WaitingPamsPin]
					LEFT JOIN 
					(
						SELECT DISTINCT
							[PamsPin] AS [DuplicatePamsPin]
						FROM [Flood].[FloodApplicationParcel]
						WHERE [ApplicationId] != @p_ApplicationId
					) DuplicateParcels ON ApplicationParcels.[PamsPin] = DuplicateParcels.[DuplicatePamsPin]
					LEFT JOIN 
					(
						SELECT DISTINCT
							[PamsPin] AS [RejectedPamsPin]
						FROM [Flood].[FloodApplicationParcel]
						WHERE [ApplicationId] != @p_ApplicationId AND [StatusId] = 7
					) RejectedParcels ON ApplicationParcels.[PamsPin] = RejectedParcels.[RejectedPamsPin]
				)
				SELECT DISTINCT
					CASE
						WHEN AP.[IsLocked] = 1
							THEN LP.[Id]
						ELSE FP.[Id]
					END AS [Id],
					AP.[ApplicationId],
					AP.[PamsPin],
					AP.[StatusId],
					AP.[PrevStatusId],
					AP.[IsLocked],
					AP.[IsApproved],
					CASE WHEN AP.[WaitingPamsPin] IS NULL THEN 0 ELSE 1 END AS [IsWaiting],
					CASE WHEN AP.[DuplicatePamsPin] IS NULL THEN 0 ELSE 1 END AS [AlreadyExists],
					CASE WHEN AP.[RejectedPamsPin] IS NULL THEN 0 ELSE 1 END AS [IsRejected],
					CASE
						WHEN AP.[IsLocked] = 1
							THEN LP.[IsValidPamsPin]
						ELSE FP.[IsValidPamsPin]
					END AS [IsValidPamsPin],
					AP.[Priority],
					AP.[ValueEstimate],
					CASE 
						WHEN AP.[StatusId] IN(4,5,6)
						THEN  PF.[HardCostFMPAmt]
						ELSE  0
    				END AS [HardCostFMPAmt],
					CASE 
						WHEN AP.[StatusId] IN(1,2,3,4)  THEN  0
								WHEN AP.[IsApproved] = 0 THEN  0
						ELSE PF.[SoftCostFMPAmt]
					END AS [SoftCostFMPAmt],
					AF.[MatchPercent] AS [ProgramMatch],
					CASE
						WHEN AP.[IsLocked] = 1
							THEN CONCAT(LP.StreetNo, ' ' , LP.StreetAddress)
						ELSE CONCAT(FP.StreetNo, ' ' , FP.StreetAddress)
					END AS [PropertyAddress],
					NULL AS [TargetArea],
					CASE
						WHEN AP.[IsLocked] = 1
							THEN LP.[Block]
						ELSE FP.[Block]
					END AS [Block],
					CASE
						WHEN AP.[IsLocked] = 1
							THEN LP.[Lot]
						ELSE FP.[Lot]
					END AS [Lot],
					CASE
						WHEN AP.[IsLocked] = 1
							THEN LP.[QualificationCode]
						ELSE FP.[QualificationCode]
					END AS [QCode],
					CASE
						WHEN AP.[IsLocked] = 1
							THEN LP.[OwnersName]
						ELSE FP.[OwnersName]
					END AS [LandOwner]
				FROM [ApplicationParcelCTE] AP
				LEFT JOIN [Flood].[FloodApplicationFinance] AF ON AP.[ApplicationId] = AF.[ApplicationId]
				LEFT JOIN [Flood].[FloodParcelFinance] PF ON AP.[ApplicationId] = PF.[ApplicationId] AND AP.PamsPin = PF.PamsPin
				LEFT JOIN [Flood].[FloodLockedParcel] LP
						ON (AP.[IsLocked] = 1 AND AP.[ApplicationId] = LP.[ApplicationId] AND AP.[PamsPin] = LP.[PamsPin])
				LEFT JOIN [Flood].[FloodParcel] FP
      					ON (AP.[IsLocked] = 0 AND AP.[PamsPin] = FP.[PamsPin]);";

    public GetApplicationParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
