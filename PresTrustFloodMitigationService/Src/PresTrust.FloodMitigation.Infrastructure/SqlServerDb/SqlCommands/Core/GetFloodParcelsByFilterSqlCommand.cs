namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFloodParcelsByFilterSqlCommand
{
    private readonly string _sqlCommand =
            @"  SELECT		TOP 50		P.[Id],
										P.[PamsPin],
										CONCAT(P.[StreetNo], ' ', P.[StreetAddress]) AS [PropertyAddress],
										ISNULL(TA.[TargetArea], 'NOT IN FLAP') AS  TargetArea,
										P.[Block],
										P.[Lot],
										P.[QualificationCode] AS [QCode],
										P.[OwnersName] AS [LandOwner],
										P.[IsValidPamsPin]
				FROM					[Flood].[FloodParcel] P
				LEFT JOIN               [Flood].[FloodFlapTargetArea] TA ON P.TargetAreaId = TA.Id
				WHERE					P.[AgencyID] = @p_AgencyId AND
										P.[PamsPin] NOT IN @p_ExistingPamsPins AND
										(@p_Block = '' OR P.[Block] like @p_Block) AND
										(@p_Lot = '' OR P.[Lot] like @p_Lot) AND
										(@p_Address = ''  OR CONCAT(P.[StreetNo], ' ', P.[StreetAddress]) like @p_Address);";

    public GetFloodParcelsByFilterSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
