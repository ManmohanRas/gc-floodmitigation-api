namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class SaveProgramManagerParcelSqlCommand
{
    private readonly string _sqlCommand =
       @"   UPDATE [Flood].[FloodParcel] SET
		    	[PamsPin] = @p_PamsPin,
		    	[IsElevated] = @p_IsElevated,
		    	[StreetNo] = @p_StreetNo,
		    	[StreetAddress] = @p_StreetAddress,
		    	[Block] = @p_Block,
		    	[Lot] = @p_Lot,
		    	[QualificationCode] = @p_QualificationCode,
		    	[Latitude] = @p_Latitude,
		    	[Longitude] = @p_Longitude,
		    	[Acreage] = @p_Acreage,
		    	[YearOfConstruction] = @p_YearOfConstruction,
		    	[SquareFootage] = @p_SquareFootage,
		    	[OwnersName] = @p_OwnersName,
		    	[OwnersAddress1] = @p_OwnersAddress1,
		    	[OwnersAddress2] = @p_OwnersAddress2,
		    	[OwnersCity] = @p_OwnersCity,
		    	[OwnersState] = @p_OwnersState,
		    	[OwnersZipcode] = @p_OwnersZipcode,
		    	[TotalAssessedValue] = @p_TotalAssessedValue,
		    	[LandValue] = @p_LandValue,
		    	[ImprovementValue] = @p_ImprovementValue,
		    	[AnnualTaxes] = @p_AnnualTaxes
		    WHERE [Id] = @p_Id;";

    public SaveProgramManagerParcelSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
