namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelListSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT
				CASE
					WHEN AP.[IsLocked] = 1
						THEN LP.PamsPin
					ELSE FP.PamsPin
				END AS [PamsPin],
				CASE
					WHEN AP.[IsLocked] = 1
						THEN CONCAT(LP.StreetNo, ' ' , LP.StreetAddress)
					ELSE CONCAT(FP.StreetNo, ' ' , FP.StreetAddress)
				END AS [PropertyAddress],
				AG.[AgencyName] AS [Municipality],
				FA.[Title] AS [ProjectArea],
				AP.[ApplicationId],
				CASE FA.[ApplicationTypeId]
					WHEN 1 THEN 'CORE'
					WHEN 2 THEN 'MATCH'
				END AS [ApplicationType],
				CASE FA.[ApplicationSubTypeId]
					WHEN 1 THEN 'DISASTER'
					WHEN 2 THEN 'FASTTRACK'
					WHEN 3 THEN 'MATCH'
					WHEN 4 THEN 'ONGOING_FLOOD'
				END AS [SubProgramType],
				PF.[SoftCostFMPAmt] + PF.[HardCostFMPAmt] AS [FinalOffer],
				AF.[MatchPercent] AS [ProgramMatch],
				CASE AP.[StatusId]
					WHEN 0 THEN 'NONE'
					WHEN 1 THEN 'SUBMITTED'
					WHEN 2 THEN 'IN_REVIEW'
					WHEN 3 THEN 'PENDING'
					WHEN 4 THEN 'APPROVED'
					WHEN 5 THEN 'PRESERVED'
					WHEN 6 THEN 'GRANT_EXPIRED'
					WHEN 7 THEN 'REJECTED'
					WHEN 8 THEN 'WITHDRAWN'
					WHEN 9 THEN 'PROJECT_AREA_EXPIRED'
					WHEN 10 THEN 'TRANSFERRED'
				END AS [PropertyStatus],
				CASE WHEN (AP.StatusId IN(1,2,3,4,5) AND (floodfeedback.FEEDBACKRESPONSE > 0 )) THEN 1 ELSE 0 END AS ShowNotification
			FROM [Flood].[FloodApplicationParcel] AP
			LEFT JOIN [Flood].[FloodLockedParcel] LP
					ON (AP.[IsLocked] = 1 AND AP.[ApplicationId] = LP.[ApplicationId] AND AP.[PamsPin] = LP.[PamsPin])
			LEFT JOIN [Flood].[FloodParcel] FP
			      	ON (AP.[IsLocked] = 0 AND AP.[PamsPin] = FP.[PamsPin])
			LEFT JOIN [Flood].[FloodApplication] FA ON AP.[ApplicationId] = FA.[Id]
			JOIN [Core].[View_AgencyEntities_FLOOD] AG ON (AP.[IsLocked] = 1 AND AG.[AgencyId] = LP.[AgencyID]) OR (AP.[IsLocked] = 0 AND AG.[AgencyId] = FP.[AgencyID])
			LEFT JOIN [Flood].[FloodParcelFinance] PF ON PF.[ApplicationId] = AP.[ApplicationId] AND PF.[PamsPin] = AP.[PamsPin]
			LEFT JOIN [Flood].[FloodApplicationFinance] AF ON AF.[Id] = FA.[Id]
			LEFT OUTER JOIN (
    SELECT
		ApplicationId,
		PamsPin,
		COUNT(PamsPin) AS FEEDBACKRESPONSE
    FROM Flood.FloodParcelFeedback
    WHERE CorrectionStatus = 'RESPONSE_RECEIVED' AND MarkRead != 1
    GROUP BY ApplicationId, PamsPin
) AS floodfeedback ON AP.ApplicationId = floodfeedback.ApplicationId AND AP.PamsPin = floodfeedback.PamsPin;";

    public GetParcelListSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
