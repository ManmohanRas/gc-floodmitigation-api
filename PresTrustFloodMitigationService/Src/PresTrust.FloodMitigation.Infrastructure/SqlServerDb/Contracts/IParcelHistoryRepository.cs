namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;
public interface IParcelHistoryRepository
{
    /// <summary>
    /// Get Parcel History
    /// </summary>
    /// <param name="parcelId"></param>
    /// <returns></returns>
    Task<List<FloodParcelHistoryEntity>> GetParcelHistoryAsync(int parcelId);

    /// <summary>
    /// Get Parcel History Item
    /// </summary>
    /// <param name="parcelId"></param>
    /// <returns></returns>
    Task<FloodParcelHistoryEntity> GetParcelHistoryItemAsync(int parcelId);

    /// <summary>
    /// Save Parcel History
    /// </summary>
    /// <param name="parcelHistory"></param>
    /// <returns></returns>
    Task<FloodParcelHistoryEntity> SaveParcelHistoryAsync(FloodParcelHistoryEntity parcelHistory);
}
    

