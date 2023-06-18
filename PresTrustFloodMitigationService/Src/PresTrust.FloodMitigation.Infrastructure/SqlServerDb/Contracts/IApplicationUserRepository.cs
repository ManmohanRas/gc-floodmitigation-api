namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationUserRepository
{
    Task<IEnumerable<FloodApplicationUserEntity>> GetPrimaryContactsAsync(int applicationId);

}
