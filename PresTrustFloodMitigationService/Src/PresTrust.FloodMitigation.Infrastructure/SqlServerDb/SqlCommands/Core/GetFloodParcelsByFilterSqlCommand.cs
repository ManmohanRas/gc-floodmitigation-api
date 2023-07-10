namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetFloodParcelsByFilterSqlCommand
{
    private readonly string _sqlCommand =
            @"  SELECT		[PAMS_PIN] AS [PamsPin],
							[PropertyLocation] AS [PropertyAddress],
							NULL AS [TargetArea],
							[Block],
							[Lot],
							[QualificationCode] AS [QCode],
							[OwnersName] AS [LandOwner]
				FROM		[Core].[Parcels]
				WHERE		[MunicipalID] = @p_AgencyId AND
							[Block] like @p_Block AND
							[Lot] like @p_Lot AND
							[PropertyLocation] like @p_Address;";

    public GetFloodParcelsByFilterSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
