namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ITechDetailsRepository
{
    /// <summary>
    /// Get Tech.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
   Task<FloodTechDetailsEntity> GetTechAsync(int applicationId);
    /// <summary>
    /// Save Tech.
    /// </summary>
    /// <param name="FloodTech"></param>
    /// <returns></returns>
    Task<FloodTechDetailsEntity> SaveTechAsync(FloodTechDetailsEntity FloodTech);
}
