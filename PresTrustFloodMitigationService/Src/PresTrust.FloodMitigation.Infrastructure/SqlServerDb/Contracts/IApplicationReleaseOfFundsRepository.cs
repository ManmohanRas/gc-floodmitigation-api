namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationReleaseOfFundsRepository
{
    Task<FloodApplicationReleaseOfFundsEntity> GetReleaseOfFundsAsync(int applicationId);

    Task<IEnumerable<FloodPropReleaseOfFundsEntity>> GetApplicationPaymentsAsync(int applicationId);

    Task<FloodApplicationReleaseOfFundsEntity> SaveAsync(FloodApplicationReleaseOfFundsEntity releaseOfFunds);

    Task<bool> ReleaseApplicationPayments(FloodPropReleaseOfFundsEntity releaseOfFunds);
}
