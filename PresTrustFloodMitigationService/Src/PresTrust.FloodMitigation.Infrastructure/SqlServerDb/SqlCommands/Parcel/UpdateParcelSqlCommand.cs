namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateParcelSqlCommand
{
    private readonly string _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[PamsPin] = @p_PamsPin,
					[AgencyId] = @p_AgencyId,
					[Block] = @p_Block,
					[Lot] = @p_Lot,
					[QualificationCode] = @p_QualificationCode,
					[StreetNo] = @p_StreetNo,
					[StreetAddress] = @p_StreetAddress,
					[OwnersName] = @p_OwnersName
				WHERE [Id] = @p_Id";

    public UpdateParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
