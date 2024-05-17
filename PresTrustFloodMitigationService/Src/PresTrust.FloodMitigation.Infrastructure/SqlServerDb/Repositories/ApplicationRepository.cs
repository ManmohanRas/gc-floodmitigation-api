using Dapper;
using PresTrust.FloodMitigation.Domain.Enums;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationRepository: IApplicationRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ApplicationRepository (
        PresTrustSqlDbContext context, 
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<List<FloodApplicationEntity>> GetApplicationsByAgenciesAsync(List<int> agencyIds, bool isExternalUser)
    {
        List<FloodApplicationEntity> results = default;
        DataTable table = new DataTable();
        table.Columns.Add("Id", typeof(int));

        if (isExternalUser)
        {
            foreach (var id in agencyIds)
            {
                var row = table.NewRow();
                row["Id"] = id;
                table.Rows.Add(row);
            }
        }

        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationsByAgenciesSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new { @p_IdTableType = table.AsTableValuedParameter("[OSTF].[IdTableType]") })).ToList();

        return results ?? new();
    }

    public async Task<FloodApplicationEntity> GetApplicationAsync(int applicationId)
    {
        FloodApplicationEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_Id = applicationId });
        result = results.FirstOrDefault();

        return result ?? new();
    }

    public async Task<FloodApplicationEntity> SaveAsync(FloodApplicationEntity application)
    {
        using var conn = context.CreateConnection();
        if(application.Id == 0)
        {
            int id = default;

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
                    @p_StatusId = application.StatusId,
                    @p_CreatedByProgramAdmin = application.CreatedByProgramAdmin,
                    @p_LastUpdatedBy = application.LastUpdatedBy
                });

            application.Id = id;
        }
        else if (application.Id > 0)
        {
            var sqlCommand = new UpdateApplicationSqlCommand();
            await conn.ExecuteAsync(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_ApplicationId = application.Id,
                    @p_Title = application.Title,
                    @p_AgencyId = application.AgencyId,
                    @p_ApplicationTypeId = application.ApplicationTypeId,
                    @p_ApplicationSubTypeId = application.ApplicationSubTypeId,
                    @p_ExpirationDate = application.ExpirationDate,
                    @p_LastUpdatedBy = application.LastUpdatedBy
                });
        }

        return application;
    }

    public async Task<bool> SaveStatusLogAsync(FloodApplicationStatusLogEntity applicationStatusLog)
    {
        bool result = false;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationStatusLogSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = applicationStatusLog.ApplicationId,
                @p_StatusId = applicationStatusLog.StatusId,
                @p_StatusDate = applicationStatusLog.StatusDate,
                @p_Notes = applicationStatusLog.Notes,
                @p_LastUpdatedBy = applicationStatusLog.LastUpdatedBy,
            });

        result = true;
        return result;
    }

    public async Task<FloodApplicationEntity> SaveApplicationWorkflowStatusAsync(FloodApplicationEntity application)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationWorkflowStatusSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = application.Id,
                @p_StatusId = application.StatusId,
                @p_LastUpdatedBy = application.LastUpdatedBy,
            });

        return application;
    }

    public async Task<List<FloodApplicationStatusLogEntity>> GetApplicationStatusLogAsync(int applicationId)
    {
        List<FloodApplicationStatusLogEntity> results = default;
       
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationStatusLogSqlCommand();
        results = (await conn.QueryAsync<FloodApplicationStatusLogEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new {
                        @p_ApplicationId = applicationId
                    })).ToList();

        return results ?? new();
    }

    public async Task<List<FloodApplicationEntity>> GetApplicationsForWarningsAsync(string applicationIds, string pamsPin, bool isTransfer = false)
    {
        List<int> appIds = applicationIds?.Split(',' , StringSplitOptions.RemoveEmptyEntries).Select(o => Convert.ToInt32(o)).ToList() ?? new ();
        List<FloodApplicationEntity> results = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationsForWarningsSqlCommand(isTransfer);
        results = (await conn.QueryAsync<FloodApplicationEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationIds = appIds,
                        @p_PamsPin = pamsPin
                    })).ToList();
        return results ?? new();
    }
}
