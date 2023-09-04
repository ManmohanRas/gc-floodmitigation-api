namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFinanceRepository
{
    /// <summary>
    ///  Procedure to fetch all finance details by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns finance.</returns>
    Task<FloodApplicationFinanceEntity> GetFinanceAsync(int applicationId);
    /// <summary>
    /// Save finance.
    /// </summary>
    /// <param name="finance"></param>
    /// <returns></returns>
    Task<FloodApplicationFinanceEntity> SaveAsync(FloodApplicationFinanceEntity finance);
}
