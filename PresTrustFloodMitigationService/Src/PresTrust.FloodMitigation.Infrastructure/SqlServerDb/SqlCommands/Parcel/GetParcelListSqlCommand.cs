namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelListSqlCommand
{
    private readonly string _sqlCommand =
       @"   SELECT
				CONCAT(FP.StreetNo, ' ',FP.StreetAddress) AS [PropertyAddress],
				AG.[AgencyName] AS [Municipality],
				FA.[Title] AS [ProjectArea],
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
				END AS [PropertyStatus]
			FROM [Flood].[FloodApplicationParcel] AP
			JOIN [Flood].[FloodParcel] FP ON AP.[PamsPin] = FP.[PamsPin]
			JOIN [Flood].[FloodApplication] FA ON AP.[ApplicationId] = FA.[Id]
			JOIN [Core].[View_AgencyEntities_FLOOD] AG ON AG.[AgencyId] = FP.[AgencyID]
			JOIN [Flood].[FloodParcelFinance] PF ON PF.[ApplicationId] = AP.[ApplicationId] AND PF.[PamsPin] = AP.[PamsPin]
			JOIN [Flood].[FloodApplicationFinance] AF ON AF.[Id] = FA.[Id];";

    public GetParcelListSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
