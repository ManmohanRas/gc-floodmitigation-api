namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ITestRepository
{
    Task<FloodTestEntity> GetTestAsync(int id);
    Task<int> SaveTestAsync(FloodTestEntity test);
}
