using PresTrust.FloodMitigation.Domain.Enums;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="agencyIds"></param>
    /// <param name="isExternalUser"></param>
    /// <returns></returns>
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
        return new FloodApplicationEntity();

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

}
