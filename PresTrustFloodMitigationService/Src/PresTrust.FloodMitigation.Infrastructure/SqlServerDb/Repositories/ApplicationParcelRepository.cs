namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationParcelRepository : IApplicationParcelRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ApplicationParcelRepository(
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task SaveAsync(List<FloodApplicationParcelEntity> applicationParcels)
    {
        using var conn = context.CreateConnection();
        foreach (var applicationParcel in applicationParcels)
        {
            var sqlCommand = new CreateApplicationParcelSqlCommand();
            await conn.ExecuteAsync(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_ApplicationId = applicationParcel.ApplicationId,
                    @p_PamsPin = applicationParcel.PamsPin,
                    @p_StatusId = applicationParcel.StatusId,
                    @p_IsLocked = applicationParcel.IsLocked
                });
        }
    }

    public async Task<IEnumerable<FloodParcelEntity>> GetApplicationPropertiesAsync(int applicationId)
    {
        IEnumerable<FloodParcelEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationParcelsSqlCommand();
        results = await conn.QueryAsync<FloodParcelEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = applicationId
                    });

        return results;
    }

    public async Task DeleteApplicationParcelsByApplicationIdAsync(int applicationId)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new DeleteApplicationParcelsByApplicationIdSqlCommand();
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
