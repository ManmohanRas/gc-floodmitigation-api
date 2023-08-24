namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IParcelFinanceRepository
{
    /// <summary>
    /// Get ParceFinance.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<FloodParcelFinanceEntity> GetParceFinanceAsync(int applicationId, string pamsPin);

    /// <summary>
    /// Save ParceFinance.
    /// </summary>
    /// <param name="parcelFinance"></param>
    /// <returns></returns>
    Task<FloodParcelFinanceEntity> SaveAsync(FloodParcelFinanceEntity parcelFinance);
}
