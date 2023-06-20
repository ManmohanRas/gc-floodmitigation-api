namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationRepository: IApplicationRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ApplicationRepository
        (
        PresTrustSqlDbContext context, 
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<FloodApplicationEntity> SaveAsync(FloodApplicationEntity application)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Title = application.Title,
                @p_AgencyId = application.AgencyId,
                @p_ApplicationTypeId = application.ApplicationTypeId,
                @p_ApplicationSubTypeId = application.ApplicationSubTypeId,
                @p_StatusId = application.StatusId
            });

        application.Id = id;

        return application;
    }

}
