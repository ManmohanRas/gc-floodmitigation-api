namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFloodParcelRepository
{
    /// <summary>
    /// Save Parcel
    /// </summary>
    /// <param name="parcels"></param>
    /// <returns></returns>
    Task SaveAsync(List<FloodParcelEntity> parcels);

    Task<FloodParcelEntity> GetParcelAsync(int applicationId, string pamsPin);
}
