namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IAnnualFundingAmountsRepository
{
    /// <summary>
    /// Get AnnualFundingDetails
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<List<FloodAnnualFundingEntity>> GetFundingDetailsAsync();

    /// <summary>
    /// Save AnnualFundingDetails
    /// </summary>
    /// <param name="FloodAnnualFundingDetails"></param>
    /// <returns></returns>
     Task<FloodAnnualFundingEntity> SaveAsync(FloodAnnualFundingEntity details);
}
