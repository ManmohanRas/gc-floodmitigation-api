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
					WHERE		[Id]=@p_ApplicationId AND [IsActive] = 1
				) FLOOD_APPLICATION
				JOIN
				(
					SELECT		[ApplicationId],
								[PamsPin],
								[StatusId],
								[IsLocked]
					FROM		[Flood].[FloodApplicationParcel]
					WHERE		[ApplicationId]=@p_ApplicationId AND [PamsPin] = @p_PamsPin
				) FLOOD_APPLICATION_PARCEL ON FLOOD_APPLICATION.[Id] = FLOOD_APPLICATION_PARCEL.[ApplicationId]
			)
			SELECT		TOP 1
						AP.[ApplicationId],
						P.[PamsPin],
						P.[AgencyId],
						P.[Block],
						P.[Lot],
						P.[QualificationCode] AS [QCode],
						P.[Latitude],
						P.[Longitude],
						P.[StreetNo],
						P.[StreetAddress],
						P.[Acreage],
						P.[OwnersName] AS [LandOwner],
						P.[OwnersAddress1],
						P.[OwnersAddress2],
						P.[OwnersCity],
						P.[OwnersState],
						P.[OwnersZipcode],
						P.[SquareFootage],
						P.[YearOfConstruction],
						P.[IsFLAP],
						P.[DateOfFLAP],
						AP.[StatusId],
						ISNULL(PSL.[StatusId], 0) AS [PrevStatusId],
						C.[CommentsJSON],
						F.[FeedbacksJSON],
						AP.[IsLocked]
			FROM		[ApplicationParcelCTE] AP
			JOIN		[Flood].[FloodParcel] P ON P.PamsPin = @p_PamsPin AND P.IsActive = 1 AND AP.PamsPin = P.PamsPin
			LEFT JOIN	[Flood].[FloodParcelStatusLog] PSL ON PSL.StatusId != AP.StatusId AND AP.ApplicationId = PSL.ApplicationId AND P.PamsPin = PSL.PamsPin
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
									WHERE		[ApplicationId] = @p_ApplicationId AND [PamsPin] = @p_PamsPin AND [CommentTypeId] IN (2)) FLOOD_COMMENT
						GROUP BY	[ApplicationId], [PamsPin]) C ON AP.ApplicationId = C.ApplicationId AND P.PamsPin = C.PamsPin
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
						GROUP BY	[ApplicationId], [PamsPin]) F ON AP.ApplicationId = F.ApplicationId AND P.PamsPin = F.PamsPin
			ORDER BY PSL.LastUpdatedOn DESC;";

    public GetParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
