namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationDetailsRepository
{
    Task<FloodApplicationDetailsEntity> GetAsync(int applicationId);

    Task<FloodApplicationDetailsEntity> SaveAsync(FloodApplicationDetailsEntity floodApplicationAdminDetails);
}
