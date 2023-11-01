namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationDetailsRepository
{
    Task<FloodApplicationAdminDetailsEntity> GetAsync(int applicationId);

    Task<FloodApplicationAdminDetailsEntity> SaveAsync(FloodApplicationAdminDetailsEntity floodApplicationAdminDetails);
}
