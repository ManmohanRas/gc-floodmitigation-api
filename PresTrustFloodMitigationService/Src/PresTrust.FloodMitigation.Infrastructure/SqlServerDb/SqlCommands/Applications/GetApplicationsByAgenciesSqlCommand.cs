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
						F.[CreatedByProgramAdmin]
					FROM [Flood].[FloodApplication] F
					JOIN [AgencyCTE] A ON F.[AgencyId] = A.[AgencyId]
					INNER JOIN @p_IdTableType tblAgencies
								ON (tblAgencies.Id = F.AgencyId)
					WHERE [IsActive] = 1
					ORDER BY [ExpirationDate] ASC;
			END
			ELSE
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
						F.[CreatedByProgramAdmin]
					FROM [Flood].[FloodApplication] F
					JOIN [AgencyCTE] A ON F.[AgencyId] = A.[AgencyId]
					WHERE [IsActive] = 1
					ORDER BY [ExpirationDate] ASC;
			END;";

    public GetApplicationsByAgenciesSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
