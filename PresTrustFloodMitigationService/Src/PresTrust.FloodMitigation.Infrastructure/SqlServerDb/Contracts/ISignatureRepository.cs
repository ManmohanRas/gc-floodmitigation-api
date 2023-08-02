namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ISignatoryRepository
{
    /// <summary>
    /// Get Signature.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<FloodSignatoryEntity> GetSignatoryAsync(int applicationId);
    /// <summary>
    /// Save Signature.
    /// </summary>
    /// <param name="floodSignatory"></param>
    /// <returns></returns>
    Task<FloodSignatoryEntity> SaveAsync(FloodSignatoryEntity floodSignatory);
}
