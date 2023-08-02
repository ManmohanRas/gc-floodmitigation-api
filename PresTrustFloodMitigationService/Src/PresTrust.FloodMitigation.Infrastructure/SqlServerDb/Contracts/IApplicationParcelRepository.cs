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
    Task<IEnumerable<FloodParcelEntity>> GetApplicationPropertiesAsync(int applicationId);
    /// <summary>
    /// Delete Application Parcels by Application Id
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task DeleteApplicationParcelsByApplicationIdAsync(int applicationId);
}
