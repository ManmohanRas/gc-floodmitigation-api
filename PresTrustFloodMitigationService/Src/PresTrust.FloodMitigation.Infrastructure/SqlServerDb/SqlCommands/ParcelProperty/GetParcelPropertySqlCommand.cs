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
    fp.[IsPreIrenePropertyOwner],
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
    fp.[LastUpdatedOn]
FROM
    [Flood].[FloodParcelProperty] fp
WHERE
    fp.[ApplicationId] = @p_ApplicationId and fp.[PamsPin] = @p_pamsPin";


    public GetParcelPropertySqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
