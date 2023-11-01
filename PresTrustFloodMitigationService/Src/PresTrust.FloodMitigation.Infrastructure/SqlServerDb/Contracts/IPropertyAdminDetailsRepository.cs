namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public interface IPropertyAdminDetailsRepository
{
    Task<FloodPropertyAdminDetailsEntity> GetAsync(int applicationId, string Pamspin);

    Task<FloodPropertyAdminDetailsEntity> SaveAsync(FloodPropertyAdminDetailsEntity floodPropertyAdminDetails);
}
