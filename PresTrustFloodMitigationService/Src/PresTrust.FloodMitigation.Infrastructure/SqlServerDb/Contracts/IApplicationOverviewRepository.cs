namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationOverviewRepository
{
    Task<FloodApplicationOverviewEntity> GetOverviewDetailsAsync(int ApplicationId);

    Task<FloodApplicationOverviewEntity> SaveAsync(FloodApplicationOverviewEntity overviewDetails);
}
