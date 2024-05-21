namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationRepository
{
    /// <summary>
    /// Procedure to fetch applications
    /// </summary>
    /// <param name="agencyIds"></param>
    /// <param name="isExternalUser"></param>
    /// <returns>Returns application collection</returns>
    Task<List<FloodApplicationEntity>> GetApplicationsByAgenciesAsync(List<int> agencyIds, bool isExternalUser);

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

    /// <summary>
    /// Save Application Status Log
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<bool> SaveStatusLogAsync(FloodApplicationStatusLogEntity applicationStatusLog);

    /// <summary>
    /// Save Application Workflow Status
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<FloodApplicationEntity> SaveApplicationWorkflowStatusAsync(FloodApplicationEntity application);

    /// <summary>
    /// Get Application Status Log
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<List<FloodApplicationStatusLogEntity>> GetApplicationStatusLogAsync(int applicationId);

    Task<List<FloodApplicationEntity>> GetApplicationsForWarningsAsync(string applicationIds, string pamsPin, bool isTransfer);
}
