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
                            AP.[WaitingApproved],
                            AP.[RejectedApproved],
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
						SELECT
							STRING_AGG(P.[ApplicationId], ',') AS [WaitingApplicationIds],
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
						GROUP BY P.[PamsPin]
					) WaitingParcels ON ApplicationParcels.[PamsPin] = WaitingParcels.[WaitingPamsPin]
					LEFT JOIN 
					(
						SELECT
							STRING_AGG([ApplicationId], ',') AS [DuplicateApplicationIds],
							[PamsPin] AS [DuplicatePamsPin]
						FROM [Flood].[FloodApplicationParcel]
						WHERE [ApplicationId] != @p_ApplicationId
						GROUP BY [PamsPin]
					) DuplicateParcels ON ApplicationParcels.[PamsPin] = DuplicateParcels.[DuplicatePamsPin]
					LEFT JOIN 
					(
						SELECT
							STRING_AGG([ApplicationId], ',') AS [RejectedApplicationIds],
							[PamsPin] AS [RejectedPamsPin]
						FROM [Flood].[FloodApplicationParcel]
						WHERE [ApplicationId] != @p_ApplicationId AND [StatusId] = 7
						GROUP BY [PamsPin]
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
					AP.[WaitingApproved],
					AP.[WaitingApplicationIds],
					CASE WHEN AP.[DuplicatePamsPin] IS NULL THEN 0 ELSE 1 END AS [AlreadyExists],
					AP.[DuplicateApplicationIds] AS [ExistingApplicationIds],
					CASE WHEN AP.[RejectedPamsPin] IS NULL THEN 0 ELSE 1 END AS [IsRejected],
					AP.[RejectedApproved],
					AP.[RejectedApplicationIds],
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
					END AS [LandOwner],
					ISNULL(TA.[TargetArea], 'NOT IN FLAP') AS  TargetArea,
					CASE WHEN (AP.StatusId IN(1,2,3,4,5) AND (floodfeedback.FEEDBACKRESPONSE > 0 )) THEN 1 ELSE 0 END AS ShowNotification
				FROM [ApplicationParcelCTE] AP
				LEFT JOIN [Flood].[FloodApplicationFinance] AF ON AP.[ApplicationId] = AF.[ApplicationId]
				LEFT JOIN [Flood].[FloodParcelFinance] PF ON AP.[ApplicationId] = PF.[ApplicationId] AND AP.PamsPin = PF.PamsPin
				LEFT JOIN [Flood].[FloodLockedParcel] LP
						ON (AP.[IsLocked] = 1 AND AP.[ApplicationId] = LP.[ApplicationId] AND AP.[PamsPin] = LP.[PamsPin])
				LEFT JOIN [Flood].[FloodParcel] FP
      					ON (AP.[IsLocked] = 0 AND AP.[PamsPin] = FP.[PamsPin])
				LEFT JOIN [Flood].[FloodFlapTargetArea] TA ON FP.TargetAreaId = TA.Id
				LEFT OUTER JOIN (
                    SELECT
					   PamsPin,
                       COUNT(PamsPin) AS FEEDBACKRESPONSE
                    FROM Flood.FloodParcelFeedback
                    WHERE CorrectionStatus = 'RESPONSE_RECEIVED' AND MarkRead != 1 AND ApplicationId = @p_ApplicationId
                    GROUP BY PamsPin
                ) AS floodfeedback ON AP.PamsPin = floodfeedback.PamsPin;";

    public GetApplicationParcelsSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
