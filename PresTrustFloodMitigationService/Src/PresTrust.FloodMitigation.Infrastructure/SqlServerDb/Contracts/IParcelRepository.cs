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
    /// Link Target Area Id To Parcel
    /// </summary>
    /// <param name="parcelIds"></param>
    /// <returns></returns>
    Task LinkTargetAreaIdToParcelAsync(List<string> pamsPin, int targetAreaId);

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

    /// <summary>
    /// Get Parcel List
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<List<FloodParcelListEntity>> GetParcelListAsync();

    /// <summary>
    /// Get Parcel List By Target Area Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<IEnumerable<FloodParcelEntity>> GetParcelsByTargetAreaIdAsync(int Id);
    
    /// <summary>
    /// Get Program Manager Parcels
    /// </summary>
    /// <returns></returns>
    Task<FloodProgramManagerParcelsEntity> GetProgramManagerParcelsAsync(int pageNumber, int pageRows, string searchBlockText, string searchLotText, string searchAddressText);

    /// <summary>
    /// Get Program Manager Parcel
    /// </summary>
    /// <param name="parcelId"></param>
    /// <returns></returns>
    Task<FloodParcelEntity> GetProgramManagerParcelAsync(int parcelId);

    /// <summary>
    /// Save Program Manager Parcel
    /// </summary>
    /// <param name="parcel"></param>
    /// <returns></returns>
    Task<FloodParcelEntity> SaveProgramManagerParcelAsync(FloodParcelEntity parcel);
}
