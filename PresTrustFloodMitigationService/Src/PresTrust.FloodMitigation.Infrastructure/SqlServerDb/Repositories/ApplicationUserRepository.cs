using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.ApplicationUsers;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationUserRepository: IApplicationUserRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ApplicationUserRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FloodApplicationUserEntity>> GetApplicationUsersAsync(int applicationId)
    {
        IEnumerable<FloodApplicationUserEntity>? results = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationUsersByApplicationIdSqlCommand();
        results = await conn.QueryAsync<FloodApplicationUserEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new { @p_ApplicationId = applicationId });

        return results;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationUsers"></param>
    /// <returns></returns>
    public async Task SaveAsync(List<FloodApplicationUserEntity> applicationUsers)
    {
        foreach (var user in applicationUsers)
        {
            int id = default;

            using var conn = context.CreateConnection();
            var sqlCommand = new CreateApplicationUserSqlCommand();
            id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_ApplicationId = user.ApplicationId,
                    @p_UserId = user.UserId,
                    @p_Email = user.Email,
                    @p_UserName = user.UserName,
                    @p_FirstName = user.FirstName,
                    @p_LastName = user.LastName,
                    @p_Title = user.Title,
                    @p_PhoneNumber = user.PhoneNumber,
                    @p_PrimaryContact = user.IsPrimaryContact,
                    @p_IsAlternateContact = user.IsAlternateContact,
                    @p_LastUpdatedBy = user.LastUpdatedBy
                });

            user.Id = id;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    public async Task DeleteApplicationUsersByApplicationIdAsync(int applicationId)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteApplicationUsersByApplicationIdSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param:
            new
            {
                @p_ApplicationId = applicationId
            });
    }
}
