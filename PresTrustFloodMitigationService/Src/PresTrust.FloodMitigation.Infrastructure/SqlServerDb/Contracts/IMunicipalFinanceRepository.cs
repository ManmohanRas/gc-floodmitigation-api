namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IMunicipalFinanceRepository
{
    /// <summary>
    /// Get MunicipalFinanceDetails
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    Task<List<FloodMunicipalFinanceEntity>> GetMunicipalFinanceDetails(int agencyId, bool useYearFilter = false);

    /// <summary>
    /// Save Municipal Finance Details
    /// </summary>
    /// <param name="muncipalFinance"></param>
    /// <returns></returns>
    Task<int> SaveMunicipalFinanceDetailsAsync(FloodMunicipalFinanceEntity muncipalFinance);
}
