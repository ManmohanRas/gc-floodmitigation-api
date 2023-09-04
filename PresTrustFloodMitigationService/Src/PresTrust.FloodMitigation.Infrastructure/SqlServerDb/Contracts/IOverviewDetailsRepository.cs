namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IOverviewDetailsRepository
{
    Task<FloodOverviewDetailsEntity> GetOverviewDetailsAsync(int ApplicationId);

    Task<FloodOverviewDetailsEntity> SaveAsync(FloodOverviewDetailsEntity overviewDetails);
}
