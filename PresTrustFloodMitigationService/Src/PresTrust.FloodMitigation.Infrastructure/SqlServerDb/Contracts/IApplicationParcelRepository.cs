namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationParcelRepository
{
    /// <summary>
    /// Save Application Parcel
    /// </summary>
    /// <param name="applicationParcels"></param>
    /// <returns></returns>
    Task SaveAsync(List<FloodApplicationParcelEntity> applicationParcels);
    /// <summary>
    /// Get Application Parcels
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<List<FloodParcelEntity>> GetApplicationPropertiesAsync(int applicationId);
    /// <summary>
    /// Delete Application Parcels by Application Id
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task DeleteApplicationParcelsByApplicationIdAsync(int applicationId);
    /// <summary>
    /// Get Application Parcels
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<FloodApplicationParcelEntity> GetApplicationPropertyAsync(int applicationId, string pamsPin);

    /// <summary>
    /// Save Application Workflow Status
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<FloodApplicationParcelEntity> SaveApplicationParcelWorkflowStatusAsync(FloodApplicationParcelEntity property);
    /// <summary>
    /// Save Application Status Log
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<bool> SaveStatusLogAsync(FloodParcelStatusLogEntity applicationStatusLog);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<List<FloodApplicationParcelEntity>> GetApplicationParcelsByApplicationIdAsync(int applicationId);

    /// <summary>
    /// Save Application Parcel SoftCost Status
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<bool> UpdateApplicationParcelSoftCostStatus(FloodApplicationParcelEntity applicationParcel);
}
