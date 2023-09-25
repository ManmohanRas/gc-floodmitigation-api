namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationSqlCommand
{
    private readonly string _sqlCommand =
       @"   WITH AgencyCTE AS (
				SELECT
					FLOOD_AGENCY.*
				FROM
				(
					SELECT		[AgencyId]
					FROM		[Flood].[FloodApplication]
					WHERE		[Id]=@p_Id AND [IsActive] = 1
				) FLOOD_APPLICATION
				JOIN
				(
					SELECT		[AgencyId],
								[AgencyName],
								(SELECT			[AgencyId] AS [Id],
												[AgencyName],
												[AgencyLabel],
												CASE [AgencyType] 
					     								WHEN 'Non-Profit' THEN 'nonprofit'
					     								WHEN 'Municipal' THEN 'municipality'
												END AS [AgencyType],
												[EntityType],
												[EntityName],
												[AddressLine1],
												[AddressLine2],
												[AddressLine3],
												[City],
												[State],
												[ZipCode]
									FOR JSON PATH,
									WITHOUT_ARRAY_WRAPPER) AS [AgencyJSON]
					FROM		[Core].[View_AgencyEntities_FLOOD]
			) FLOOD_AGENCY ON FLOOD_APPLICATION.[AgencyId] = FLOOD_AGENCY.[AgencyId])
			SELECT		TOP 1
						A.[Id],
						A.[AgencyId],
						AG.[AgencyName],
						A.[Title],
						A.[ApplicationTypeId],
						A.[ApplicationSubTypeId],
						A.[ExpirationDate],
						A.[StatusId],
						ISNULL(ASL.[StatusId], 0) AS [PrevStatusId],
						A.[CreatedByProgramAdmin],
						AG.[AgencyJSON],
						AO.[NoOfHomes],
						AO.[NoOfContiguousHomes],
						C.[CommentsJSON],
						F.[FeedbacksJSON],
						A.[LastUpdatedOn]
			FROM		[Flood].[FloodApplication] A
			LEFT JOIN	[Flood].[FloodApplicationStatusLog] ASL ON ASL.StatusId != A.StatusId AND A.Id = ASL.ApplicationId
			JOIN		[AgencyCTE] AG ON A.[AgencyId] = AG.[AgencyId]
			LEFT JOIN	[Flood].[FloodOverview] AO ON A.Id = AO.ApplicationId
			LEFT JOIN	(SELECT		[ApplicationId],
									CONCAT('[', STRING_AGG([CommentJSON], ','), ']') AS [CommentsJSON]
						FROM		(SELECT		[ApplicationId],
												(SELECT		[Id],
															[ApplicationId],
															[CommentTypeId],
															[Comment],
															[LastUpdatedOn]
													FOR JSON PATH,
													WITHOUT_ARRAY_WRAPPER) AS [CommentJSON]
									FROM		[Flood].[FloodApplicationComment]
									WHERE		[ApplicationId] = @p_Id AND [CommentTypeId] IN (2)) FLOOD_COMMENT
						GROUP BY	[ApplicationId]) C ON A.Id = C.ApplicationId
			LEFT JOIN	(SELECT		[ApplicationId],
									CONCAT('[', STRING_AGG([FeedbackJSON], ','), ']') AS [FeedbacksJSON]
						FROM		(SELECT		[ApplicationId],
												(SELECT		[Id],
															[ApplicationId],
															[SectionId],
															[Feedback],
															[RequestForCorrection],
															[CorrectionStatus],
															[MarkRead],
															[LastUpdatedOn]
													FOR JSON PATH,
													WITHOUT_ARRAY_WRAPPER) AS [FeedbackJSON]
									FROM		[Flood].[FloodApplicationFeedback]
									WHERE		[ApplicationId] = @p_Id) FLOOD_FEEDBACK
						GROUP BY	[ApplicationId]) F ON A.Id = F.ApplicationId
			WHERE		A.[Id]=@p_Id AND A.[IsActive] = 1
			ORDER BY ASL.LastUpdatedOn DESC;";

    public GetApplicationSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
