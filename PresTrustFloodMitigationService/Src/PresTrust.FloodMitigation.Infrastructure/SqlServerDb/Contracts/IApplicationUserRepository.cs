namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationUserRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<IEnumerable<FloodApplicationUserEntity>> GetApplicationUsersAsync(int applicationId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationUsers"></param>
    /// <returns></returns>
    Task SaveAsync(List<FloodApplicationUserEntity> applicationUsers);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task DeleteApplicationUsersByApplicationIdAsync(int applicationId);

}
