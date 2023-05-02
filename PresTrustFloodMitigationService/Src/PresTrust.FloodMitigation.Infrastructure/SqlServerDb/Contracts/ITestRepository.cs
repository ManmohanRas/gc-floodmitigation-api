namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts
{
    public interface ITestRepository
    {
        Task<FlmitigTestEntity> GetTestAsync(int id);
        Task<int> SaveTestAsync(FlmitigTestEntity test);
    }
}
