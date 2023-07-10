namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFloodParcelsByFilterSqlCommand
{
    private readonly string _sqlCommand =
            @"  SELECT		TOP 20		[PAMS_PIN] AS [PamsPin],
										[PropertyLocation] AS [PropertyAddress],
										NULL AS [TargetArea],
										[Block],
										[Lot],
										[QualificationCode] AS [QCode],
										[OwnersName] AS [LandOwner]
				FROM					[Core].[Parcels]
				WHERE					[MunicipalID] = @p_AgencyId AND
										(@p_Block = '' OR [Block] like @p_Block) AND
										(@p_Lot = '' OR [Lot] like @p_Lot) AND
										(@p_Address = '' OR [PropertyLocation] like @p_Address);";

    public GetFloodParcelsByFilterSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
