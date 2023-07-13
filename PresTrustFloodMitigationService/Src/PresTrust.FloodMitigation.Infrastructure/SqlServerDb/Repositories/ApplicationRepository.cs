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

    public async Task<IEnumerable<FloodApplicationEntity>> GetApplicationsByAgenciesAsync(List<int> agencyIds, bool isExternalUser)
    {
        IEnumerable<FloodApplicationEntity> results = default;
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
        string sqlCommand = new GetApplicationsByAgenciesSqlCommand().ToString();
        results = await conn.QueryAsync<FloodApplicationEntity>(sqlCommand,
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new { @p_IdTableType = table.AsTableValuedParameter("[OSTF].[IdTableType]") });

        return results;
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

        return result;
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
                @p_StatusId = application.StatusId,
                @p_CreatedByProgramAdmin = application.CreatedByProgramAdmin,
                @p_LastUpdatedBy = application.LastUpdatedBy,
            });

        application.Id = id;

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
}
