namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public interface IPropertyAdminDetailsRepository
{
    Task<FloodPropertyAdminDetailsEntity> GetAsync(int applicationId, string PamsPin);

    Task<FloodPropertyAdminDetailsEntity> SaveAsync(FloodPropertyAdminDetailsEntity floodPropertyAdminDetails);
}
