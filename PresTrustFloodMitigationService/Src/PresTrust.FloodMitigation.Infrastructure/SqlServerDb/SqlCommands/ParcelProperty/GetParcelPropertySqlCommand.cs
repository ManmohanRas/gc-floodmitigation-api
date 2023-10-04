namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class GetParcelPropertySqlCommand
{
    private readonly string _sqlCommand =
      @" SELECT
    fp.[Id],
    fp.[ApplicationId],
    fp.[PamsPin],
    fp.[Priority],
    fp.[ValueEstimate],
    fp.[EstimatedPurchasePrice],
    fp.[TotalAssessedValue],
    fp.[LandValue],
    fp.[ImprovementValue],
    fp.[AnnualTaxes],
    fp.[BRV],
    fp.[NfipPolicyNo],
    fp.[SourceOfValueEstimate],
    fp.[FirstFloorElevation],
    fp.[StructureType],
    fp.[FoundationType],
    fp.[OccupancyClass],
    fp.[PercentageOfDamage],
    fp.[HasContaminants],
    fp.[IsLowIncomeHousing],
    fp.[HasHistoricSignificance],
    fp.[IsRentalProperty],
    fp.[RentPerMonth],
    fp.[NeedSoftCost],
    fp.[LastUpdatedBy],
    fp.[LastUpdatedOn],
    fp2.[AgencyID],
    fp2.[Block],
    fp2.[Lot],
    fp2.[QualificationCode],
    fp2.[Latitude],
    fp2.[Longitude],
    fp2.[StreetNo],
    fp2.[StreetAddress],
    fp2.[Acreage],
    fp2.[OwnersName],
    fp2.[OwnersAddress1],
    fp2.[OwnersAddress2],
    fp2.[OwnersCity],
    fp2.[OwnersState],
    fp2.[OwnersZipcode],
    fp2.[SquareFootage],
    fp2.[YearOfConstruction],
    fp2.[IsFLAP],
    fp2.[DateOfFLAP],
    fp2.[IsActive]
FROM
    [Flood].[FloodParcelProperty] fp
JOIN
    [Flood].[FloodParcel] fp2 ON fp.[PamsPin] = fp2.[PamsPin]
WHERE
    fp.[ApplicationId] = @p_ApplicationId and fp2.[PamsPin] = @p_pamsPin";


    public GetParcelPropertySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
