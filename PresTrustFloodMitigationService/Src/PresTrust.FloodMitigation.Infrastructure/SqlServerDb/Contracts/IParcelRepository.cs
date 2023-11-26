namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IParcelRepository
{
    /// <summary>
    /// Save Parcel
    /// </summary>
    /// <param name="parcels"></param>
    /// <returns></returns>
    Task SaveParcelsAsync(List<FloodParcelEntity> parcels);

    /// <summary>
    /// Get Parcel
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<FloodParcelEntity> GetParcelAsync(int applicationId, string pamsPin);

    /// <summary>
    /// Get Parcel Status Log
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<List<FloodParcelStatusLogEntity>> GetParcelStatusLogAsync(int applicationId, string pamsPin);
    /// <summary>
    /// Update Parcel
    /// </summary>
    /// <param name="parcels"></param>
    /// <returns></returns>
    Task<FloodParcelEntity> UpdateAsync(FloodParcelEntity parcels);

}
