namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationRepository
{
    /// <summary>
    /// Get Application by Id
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<FloodApplicationEntity> GetApplicationAsync(int applicationId);

    /// <summary>
    /// Save Application
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<FloodApplicationEntity> SaveAsync(FloodApplicationEntity application);
}
