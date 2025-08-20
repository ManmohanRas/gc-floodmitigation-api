namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetApplicationsByAgenciesSqlCommand
{
    private readonly string _sqlCommand =
       @"   IF(EXISTS(SELECT TOP(1) * FROM @p_IdTableType))
			BEGIN
				WITH AgencyCTE AS (
						SELECT 
							FA.[AgencyId],
							AG.[AgencyName]
						FROM
						(
							SELECT DISTINCT
								[AgencyId]
							FROM [Flood].[FloodApplication]
							WHERE [IsActive] = 1
						) FA
						JOIN [Core].[View_AgencyEntities_FLOOD] AG
							ON FA.AgencyId = AG.AgencyId)
					SELECT 
						F.[Id],
						F.[AgencyId],
						A.[AgencyName],
						F.[Title],
						F.[ApplicationTypeId],
						F.[ApplicationSubTypeId],
						F.[ExpirationDate],
						F.[StatusId],
						F.[CreatedByProgramAdmin],
                        CASE WHEN (F.StatusId IN(2,4,5,6) AND (floodfeedback.FEEDBACKRESPONSE > 0 )) THEN 1 ELSE 0 END AS ShowNotification
					FROM [Flood].[FloodApplication] F
                    LEFT OUTER JOIN (
                                       SELECT 
                                           ApplicationId,
                                           COUNT(ApplicationId) AS FEEDBACKRESPONSE
                                       FROM Flood.FloodApplicationFeedback
                                       WHERE CorrectionStatus = 'RESPONSE_RECEIVED' AND MarkRead != 1
                                       GROUP BY ApplicationId
                                     ) AS floodfeedback ON F.Id = floodfeedback.ApplicationId
					JOIN [AgencyCTE] A ON F.[AgencyId] = A.[AgencyId]
					INNER JOIN @p_IdTableType tblAgencies
								ON (tblAgencies.Id = F.AgencyId)
					WHERE F.[IsActive] = 1
					ORDER BY F.[ExpirationDate] ASC;
			END
			ELSE
			BEGIN
					WITH AgencyCTE AS (
						SELECT DISTINCT
							FA.[AgencyId],
							AG.[AgencyName]
						FROM
						(
							SELECT DISTINCT
								[AgencyId]
							FROM [Flood].[FloodApplication]
							WHERE [IsActive] = 1
						) FA
						JOIN [Core].[View_AgencyEntities_FLOOD] AG
							ON FA.AgencyId = AG.AgencyId)
					SELECT 
						F.[Id],
						F.[AgencyId],
						A.[AgencyName],
						F.[Title],
						F.[ApplicationTypeId],
						F.[ApplicationSubTypeId],
						F.[ExpirationDate],
						F.[StatusId],
						F.[CreatedByProgramAdmin],
                        CASE WHEN (F.StatusId IN(2,4,5,6) AND (floodfeedback.FEEDBACKRESPONSE > 0 )) THEN 1 ELSE 0 END AS ShowNotification
					FROM [Flood].[FloodApplication] F
                    LEFT OUTER JOIN (
                                        SELECT
                                           ApplicationId,
                                           COUNT(ApplicationId) AS FEEDBACKRESPONSE
                                        FROM Flood.FloodApplicationFeedback
                                        WHERE CorrectionStatus = 'RESPONSE_RECEIVED' AND MarkRead != 1
                                        GROUP BY ApplicationId
                                    ) AS floodfeedback ON F.Id = floodfeedback.ApplicationId
					JOIN [AgencyCTE] A ON F.[AgencyId] = A.[AgencyId]
					WHERE F.[IsActive] = 1
					ORDER BY F.[ExpirationDate] ASC;
			END;";

    public GetApplicationsByAgenciesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
