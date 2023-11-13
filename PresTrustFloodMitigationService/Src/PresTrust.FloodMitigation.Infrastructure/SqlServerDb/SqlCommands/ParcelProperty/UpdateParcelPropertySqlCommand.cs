namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class UpdateParcelPropertySqlCommand
{
    private readonly string _sqlCommand =
     @"UPDATE [Flood].[FloodParcelProperty]
						SET
							[ApplicationId] = @p_ApplicationId,
							[PamsPin] = @p_PamsPin,
							[Priority] = @p_Priority,
							[ValueEstimate] = @p_ValueEstimate,
							[EstimatedPurchasePrice] = @p_EstimatedPurchasePrice,
							[TotalAssessedValue] = @p_TotalAssessedValue,
							[LandValue] = @p_LandValue,
							[ImprovementValue] = @p_ImprovementValue,
							[AnnualTaxes] = @p_AnnualTaxes,
							[BRV] = @p_BRV,
							[NfipPolicyNo] = @p_NfipPolicyNo,
							[SourceOfValueEstimate] = @p_SourceOfValueEstimate,
							[FirstFloorElevation] = @p_FirstFloorElevation,
							[StructureType] = @p_StructureType,
							[FoundationType] = @p_FoundationType,
							[OccupancyClass] = @p_OccupancyClass,
							[PercentageOfDamage] = @p_PercentageOfDamage,
							[HasContaminants] = @p_HasContaminants,
							[IsLowIncomeHousing] = @p_IsLowIncomeHousing,
							[HasHistoricSignificance] = @p_HasHistoricSignificance,
							[IsRentalProperty] = @p_IsRentalProperty,
							[RentPerMonth] = @p_RentPerMonth,
							[NeedSoftCost] = @p_NeedSoftCost,
							[LastUpdatedBy] = @p_LastUpdatedBy,
							[LastUpdatedOn] = @p_LastUpdatedOn
                            WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId AND PamsPin = @p_PamsPin";

    public UpdateParcelPropertySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
