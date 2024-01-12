namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IMunicipalTrustFundPermittedUsesRepository
{
    /// Save MunicipalTrustFund Permitted Uses.
    /// </summary>
    /// <param name="HistMunicipalTrustFundPermittedUses"></param>
    /// <returns></returns>
    public Task<int> SaveAsync(FloodMunicipalTrustFundPermittedUsesEntity histMunicipalTrustFundPermittedUses);

    /// <summary>
    /// Get MunicipalTrustFundPermittedUses.
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    Task<FloodMunicipalTrustFundPermittedUsesEntity> GetMunicipalTrustFundPermittedUses(int agencyId);

    /// <summary>
    /// update year of inception
    /// </summary>
    /// <param name="yearOfInception"></param>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    Task UpdateYearOfInception(string yearOfInception, int agencyId);
}
