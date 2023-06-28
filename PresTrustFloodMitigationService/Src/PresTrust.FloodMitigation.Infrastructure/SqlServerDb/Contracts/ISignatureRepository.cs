namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ISignatureRepository
{
    /// <summary>
    /// Get Signature.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<FloodSignatoryEntity> GetSignatureAsync(int applicationId);
    /// <summary>
    /// Save Signature.
    /// </summary>
    /// <param name="floodSignature"></param>
    /// <returns></returns>
    Task<FloodSignatoryEntity> SaveAsync(FloodSignatoryEntity floodSignature);
}
