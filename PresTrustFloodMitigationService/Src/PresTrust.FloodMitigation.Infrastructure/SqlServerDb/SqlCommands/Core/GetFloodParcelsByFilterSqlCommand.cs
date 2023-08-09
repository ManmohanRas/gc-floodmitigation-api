namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFloodParcelsByFilterSqlCommand
{
    private readonly string _sqlCommand =
            @"  SELECT		TOP 50		[PamsPin],
										CONCAT([StreetNo], ' ', [StreetAddress]) AS [PropertyAddress],
										NULL AS [TargetArea],
										[Block],
										[Lot],
										[QualificationCode] AS [QCode],
										[OwnersName] AS [LandOwner]
				FROM					[Flood].[FloodParcel]
				WHERE					[AgencyID] = @p_AgencyId AND
										[PamsPin] NOT IN @p_ExistingPamsPins AND
										(@p_Block = '' OR [Block] like @p_Block) AND
										(@p_Lot = '' OR [Lot] like @p_Lot) AND
										(@p_Address = ''  OR CONCAT([StreetNo], ' ', [StreetAddress]) like @p_Address);";

    public GetFloodParcelsByFilterSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
