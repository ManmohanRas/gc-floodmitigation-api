namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IParcelTrackingRepository
{
    Task<FloodParcelTrackingEntity> GetTrackingAsync(int applicationId, string pamsPin);
    Task<FloodParcelTrackingEntity> SaveAsync(FloodParcelTrackingEntity parcelTracking);
}
