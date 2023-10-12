namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateParcelPropertySqlCommand
{
    private readonly string _sqlCommand =
                 @"INSERT INTO [Flood].[FloodParcelProperty]
(
    [ApplicationId],
    [PamsPin],
    [Priority],
    [ValueEstimate],
    [EstimatedPurchasePrice],
    [TotalAssessedValue],
    [LandValue],
    [ImprovementValue],
    [AnnualTaxes],
    [BRV],
    [NfipPolicyNo],
    [SourceOfValueEstimate],
    [FirstFloorElevation],
    [StructureType],
    [FoundationType],
    [OccupancyClass],
    [PercentageOfDamage],
    [HasContaminants],
    [IsLowIncomeHousing],
    [HasHistoricSignificance],
    [IsRentalProperty],
    [RentPerMonth],
    [NeedSoftCost],
    [LastUpdatedBy],
    [LastUpdatedOn]
)
VALUES
(
    @p_ApplicationId,
    @p_PamsPin,
    @p_Priority,
    @p_ValueEstimate,
    @p_EstimatedPurchasePrice,
    @p_TotalAssessedValue,
    @p_LandValue,
    @p_ImprovementValue,
    @p_AnnualTaxes,
    @p_BRV,
    @p_NfipPolicyNo,
    @p_SourceOfValueEstimate,
    @p_FirstFloorElevation,
    @p_StructureType,
    @p_FoundationType,
    @p_OccupancyClass,
    @p_PercentageOfDamage,
    @p_HasContaminants,
    @p_IsLowIncomeHousing,
    @p_HasHistoricSignificance,
    @p_IsRentalProperty,
    @p_RentPerMonth,
    @p_NeedSoftCost,
    @p_LastUpdatedBy,
    GETDATE()
);
SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateParcelPropertySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
