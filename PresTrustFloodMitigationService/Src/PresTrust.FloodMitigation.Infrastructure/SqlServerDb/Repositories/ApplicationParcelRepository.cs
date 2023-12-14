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

    public async Task<List<FloodParcelEntity>> GetApplicationPropertiesAsync(int applicationId)
    {
        List<FloodParcelEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationParcelsSqlCommand();
        results = (await conn.QueryAsync<FloodParcelEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = applicationId
                    })).ToList();

        return results ?? new();
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


    public async Task<FloodApplicationParcelEntity> GetApplicationPropertyAsync(int applicationId, string pamsPin )
    {
        FloodApplicationParcelEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationParcelSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationParcelEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = applicationId,
                        @p_PamsPin = pamsPin
                    });

        result = results.FirstOrDefault();

        return result ?? new();
    }


    public async Task<FloodApplicationParcelEntity> SaveApplicationParcelWorkflowStatusAsync(FloodApplicationParcelEntity property)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationParcelWorkflowStatusSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = property.ApplicationId,
                @p_PamsPin = property.PamsPin,
                @p_StatusId = property.StatusId,
                @p_LastUpdatedBy = property.LastUpdatedBy,
            });
        return property;
    }


    public async Task<bool> SaveStatusLogAsync(FloodParcelStatusLogEntity applicationStatusLog)
    {
        bool result = false;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelStatusLogSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationStatusLog.ApplicationId,
                @p_PamsPin = applicationStatusLog.PamsPin,
                @p_StatusId = applicationStatusLog.StatusId,
                @p_StatusDate = applicationStatusLog.StatusDate,
                @p_Notes = applicationStatusLog.Notes,
                @p_LastUpdatedBy = applicationStatusLog.LastUpdatedBy,
            });

        result = true;
        return result;
    }

    public async Task<List<FloodApplicationParcelEntity>> GetApplicationParcelsByApplicationIdAsync(int applicationId)
    {
        List<FloodApplicationParcelEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationParcelsSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationParcelEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = applicationId
                    })).ToList();

        return results ?? new();
    }

    public async Task<bool> UpdateApplicationParcelSoftCostStatus(FloodApplicationParcelEntity applicationParcel)
    {
        bool result = false;

        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationParcelSoftCostStatusSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationParcel.ApplicationId,
                @p_PamsPin = applicationParcel.PamsPin,
                @p_IsSubmitted = applicationParcel.IsSubmitted,
                @p_IsApproved = applicationParcel.IsApproved
            });

        result = true;
        return result;
    }
}
