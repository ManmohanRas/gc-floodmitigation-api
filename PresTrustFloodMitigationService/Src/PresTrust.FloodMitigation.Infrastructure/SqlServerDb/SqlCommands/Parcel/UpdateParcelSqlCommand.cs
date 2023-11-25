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

    public UpdateParcelSqlCommand(bool updatePropertyTab = false)
    {
        if(updatePropertyTab)
        {
            _sqlCommand =
            @"  UPDATE [Flood].[FloodParcel] SET
					[PamsPin] = @p_PamsPin,
					[AgencyId] = @p_AgencyId,
					[Block] = @p_Block,
					[Lot] = @p_Lot,
					[QualificationCode] = @p_QualificationCode,
					[StreetNo] = @p_StreetNo,
					[StreetAddress] = @p_StreetAddress,
					[OwnersName] = @p_OwnersName,
					[Latitude] = @p_Latitude,
					[Longitude] = @p_Longitude,
					[Acreage] = @p_Acreage,
					[OwnersAddress1] = @p_OwnersAddress1,
					[OwnersAddress2] = @p_OwnersAddress2,
					[OwnersCity] = @p_OwnersCity,
					[OwnersState] = @p_OwnersState,
					[OwnersZipcode] = @p_OwnersZipcode,
					[SquareFootage] = @p_SquareFootage,
					[YearOfConstruction] = @p_YearOfConstruction,
					[TotalAssessedValue] = @p_TotalAssessedValue,
					[LandValue] = @p_LandValue,
					[ImprovementValue] = @p_ImprovementValue,
					[AnnualTaxes] = @p_AnnualTaxes
				WHERE [PamsPin] = @p_PamsPin";
        }
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
