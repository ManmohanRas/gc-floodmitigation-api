namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelSqlCommand
{
    private readonly string _sqlCommand =
       @"   WITH ApplicationParcelCTE AS (
				SELECT
					FLOOD_APPLICATION_PARCEL.*
				FROM
				(
					SELECT		[Id]
					FROM		[Flood].[FloodApplication]
					WHERE		[Id] = @p_ApplicationId AND [IsActive] = 1
				) FLOOD_APPLICATION
				JOIN
				(
					SELECT		[ApplicationId],
								[PamsPin],
								[StatusId],
								[IsLocked],
								ISNULL([IsApproved],0) AS IsApproved
					FROM		[Flood].[FloodApplicationParcel]
					WHERE		[ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin
				) FLOOD_APPLICATION_PARCEL ON FLOOD_APPLICATION.[Id] = FLOOD_APPLICATION_PARCEL.[ApplicationId]
			)
			SELECT		TOP 1
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[Id]
							ELSE FP.[Id]
						END AS [ParcelId],
						AP.[ApplicationId],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[PamsPin]
							ELSE FP.[PamsPin]
						END AS [PamsPin],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[AgencyId]
							ELSE FP.[AgencyId]
						END AS [AgencyId],
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
								THEN LP.[Latitude]
							ELSE FP.[Latitude]
						END AS [Latitude],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[Longitude]
							ELSE FP.[Longitude]
						END AS [Longitude],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[StreetNo]
							ELSE FP.[StreetNo]
						END AS [StreetNo],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[StreetAddress]
							ELSE FP.[StreetAddress]
						END AS [StreetAddress],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[Acreage]
							ELSE FP.[Acreage]
						END AS [Acreage],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[OwnersName]
							ELSE FP.[OwnersName]
						END AS [LandOwner],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[OwnersAddress1]
							ELSE FP.[OwnersAddress1]
						END AS [OwnersAddress1],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[OwnersAddress2]
							ELSE FP.[OwnersAddress2]
						END AS [OwnersAddress2],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[OwnersCity]
							ELSE FP.[OwnersCity]
						END AS [OwnersCity],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[OwnersState]
							ELSE FP.[OwnersState]
						END AS [OwnersState],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[OwnersZipcode]
							ELSE FP.[OwnersZipcode]
						END AS [OwnersZipcode],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[SquareFootage]
							ELSE FP.[SquareFootage]
						END AS [SquareFootage],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[YearOfConstruction]
							ELSE FP.[YearOfConstruction]
						END AS [YearOfConstruction],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[TotalAssessedValue]
							ELSE FP.[TotalAssessedValue]
						END AS [TotalAssessedValue],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[LandValue]
							ELSE FP.[LandValue]
						END AS [LandValue],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[ImprovementValue]
							ELSE FP.[ImprovementValue]
						END AS [ImprovementValue],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[AnnualTaxes]
							ELSE FP.[AnnualTaxes]
						END AS [AnnualTaxes],
						CASE
							WHEN AP.[IsLocked] = 1 AND LP.[TargetAreaId] > 0 THEN 1
							WHEN AP.[IsLocked] = 0 AND FP.[TargetAreaId] > 0 THEN 1
							ELSE 0 END AS [IsFLAP],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[DateOfFLAP]
							ELSE FP.[DateOfFLAP]
						END AS [DateOfFLAP],
						CASE
							WHEN AP.[IsLocked] = 1
								THEN LP.[IsValidPamsPin]
							ELSE FP.[IsValidPamsPin]
						END AS [IsValidPamsPin],
						AP.[StatusId],
						ISNULL(PSL.[StatusId], 0) AS [PrevStatusId],
						C.[CommentsJSON],
						F.[FeedbacksJSON],
						AP.[IsLocked],
						ISNULL([IsApproved],0) AS [IsApproved],
						ISNULL(TA.[TargetArea], 'NOT IN FLAP') AS [TargetArea],
						OtherLocked.[ApplicationId] AS [LockedAnotherApplicationId]
			FROM		[ApplicationParcelCTE] AP
			LEFT JOIN [Flood].[FloodLockedParcel] LP
					ON (LP.PamsPin = @p_PamsPin AND LP.IsActive = 1 AND AP.[IsLocked] = 1 AND AP.[ApplicationId] = LP.[ApplicationId] AND AP.[PamsPin] = LP.[PamsPin])
			LEFT JOIN [Flood].[FloodParcel] FP
      				ON (FP.PamsPin = @p_PamsPin AND FP.IsActive = 1 AND AP.[IsLocked] = 0 AND AP.[PamsPin] = FP.[PamsPin])
			LEFT JOIN [Flood].[FloodFlapTargetArea] TA 
					ON FP.TargetAreaId = TA.Id
			LEFT JOIN	[Flood].[FloodParcelStatusLog] PSL ON PSL.StatusId != AP.StatusId AND AP.ApplicationId = PSL.ApplicationId AND ((AP.[IsLocked] = 1 AND LP.PamsPin = PSL.PamsPin) OR (AP.[IsLocked] = 0 AND FP.PamsPin = PSL.PamsPin))
			LEFT JOIN	(SELECT		[ApplicationId],
									[PamsPin],
									CONCAT('[', STRING_AGG([CommentJSON], ','), ']') AS [CommentsJSON]
						FROM		(SELECT		[ApplicationId],
												[PamsPin],
												(SELECT		[Id],
															[ApplicationId],
															[PamsPin],
															[CommentTypeId],
															[Comment],
															[LastUpdatedOn]
													FOR JSON PATH,
													WITHOUT_ARRAY_WRAPPER) AS [CommentJSON]
									FROM		[Flood].[FloodParcelComment]
									WHERE		[ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin) FLOOD_COMMENT
						GROUP BY	[ApplicationId], [PamsPin]) C ON AP.ApplicationId = C.ApplicationId AND ((AP.[IsLocked] = 1 AND LP.PamsPin = C.PamsPin) OR (AP.[IsLocked] = 0 AND FP.PamsPin = C.PamsPin))
			LEFT JOIN	(SELECT		[ApplicationId],
									[PamsPin],
									CONCAT('[', STRING_AGG([FeedbackJSON], ','), ']') AS [FeedbacksJSON]
						FROM		(SELECT		[ApplicationId],
												[PamsPin],
												(SELECT		[Id],
															[ApplicationId],
															[PamsPin],
															[SectionId],
															[Feedback],
															[RequestForCorrection],
															[CorrectionStatus],
															[MarkRead],
															[LastUpdatedOn]
													FOR JSON PATH,
													WITHOUT_ARRAY_WRAPPER) AS [FeedbackJSON]
									FROM		[Flood].[FloodParcelFeedback]
									WHERE		[ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin) FLOOD_FEEDBACK
						GROUP BY	[ApplicationId], [PamsPin]) F ON AP.ApplicationId = F.ApplicationId AND ((AP.[IsLocked] = 1 AND LP.PamsPin = F.PamsPin) OR (AP.[IsLocked] = 0 AND FP.PamsPin = F.PamsPin))
			LEFT JOIN	(SELECT TOP 1		LP.ApplicationId,
											LP.PamsPin
						FROM Flood.FloodApplicationParcel AP
						JOIN Flood.FloodLockedParcel LP ON AP.ApplicationId = LP.ApplicationId AND AP.PamsPin = LP.PamsPin
						WHERE AP.ApplicationId != @p_ApplicationId AND AP.PamsPin = @p_PamsPin AND AP.IsLocked = 1
						ORDER BY LP.LastUpdatedOn DESC) OtherLocked ON AP.PamsPin = OtherLocked.PamsPin
			ORDER BY PSL.StatusDate DESC;";

    public GetParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
