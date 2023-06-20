namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationRepository
{
    /// <summary>
    /// Save Application
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    Task<FloodApplicationEntity> SaveAsync(FloodApplicationEntity application);
}
