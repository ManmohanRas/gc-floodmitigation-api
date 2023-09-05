namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  IF NOT EXISTS(SELECT [PamsPin] FROM [Flood].[FloodParcel] WHERE [PamsPin] = @p_PamsPin)
				BEGIN
					INSERT INTO [Flood].[FloodParcel]
					(
						[PamsPin],
						[AgencyId],
						[Block],
						[Lot],
						[QualificationCode],
						[StreetNo],
						[StreetAddress],
						[OwnersName]
					)
					VALUES
					(
						@p_PamsPin,
						@p_AgencyId,
						@p_Block,
						@p_Lot,
						@p_QualificationCode,
						@p_StreetNo,
						@p_StreetAddress,
						@p_OwnersName
					);
				END;";

    public CreateParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
