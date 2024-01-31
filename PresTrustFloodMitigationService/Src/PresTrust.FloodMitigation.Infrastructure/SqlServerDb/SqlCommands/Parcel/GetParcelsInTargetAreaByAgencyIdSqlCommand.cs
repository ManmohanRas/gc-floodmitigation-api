namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelsInTargetAreaByAgencyIdSqlCommand
{
    private readonly string _sqlCommand =
                @"  			SELECT				                    
										[PamsPin],
										[AgencyID],
										[Block],
										[Lot],
										[QualificationCode],
										[StreetNo],
										[StreetAddress],
										[OwnersName] AS [LandOwner],
										[TargetAreaId],
										(SELECT TargetArea FROM [Flood].[FloodFlapTargetArea] WHERE Id = TargetAreaId) AS TargetArea
				FROM					[Flood].[FloodParcel]
				WHERE					[AgencyID] = @p_AgencyId AND
										[TargetAreaId] IS NOT NULL;";

    public GetParcelsInTargetAreaByAgencyIdSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
