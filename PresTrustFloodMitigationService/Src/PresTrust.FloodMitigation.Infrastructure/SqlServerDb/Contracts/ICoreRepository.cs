namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ICoreRepository
{
    /// <summary>
    /// Procedure to get list of agencies
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FloodAgencyEntity>> GetAgenciesAsync();
    /// <summary>
    /// Procedure to get agency by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<FloodAgencyEntity> GetAgencyByIdAsync(int id);
    /// <summary>
    /// Procedure to validate PamsPin
    /// </summary>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<bool> IsValidPamsPinAsync(string pamsPin);
    /// <summary>
    /// Procedure to get list of Parcels
    /// </summary>
    /// <param name="agencyId"></param>
    /// <param name="block"></param>
    /// <param name="lot"></param>
    /// <param name="address"></param>
    /// <returns></returns>
    Task<IEnumerable<FloodParcelEntity>> GetFloodParcelsByFilterAsync(int agencyId, string block, string lot, string address);
}
