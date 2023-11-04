namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ApplicationSignatoryRepository : IApplicationSignatoryRepository
{
    #region " Members ... "

    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    #endregion

    #region " ctor ... "

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="systemParamConfigOptions"></param>
    public ApplicationSignatoryRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion



    /// <summary>
    ///  Procedure to fetch Signatory details by Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns FloodApplicationSignatoryEntity.</returns>
    public async Task<FloodApplicationSignatoryEntity> GetSignatoryAsync(int applicationId)
        {
            FloodApplicationSignatoryEntity result = default;
            using var conn = context.CreateConnection();
            var sqlCommand = new GetApplicationSignatorySqlCommand();
            var results = await conn.QueryAsync<FloodApplicationSignatoryEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new { 
                                    @p_ApplicationId = applicationId
                                   });

            result = results.FirstOrDefault();

            return result ?? new();
    }

    /// <summary>
    /// Save Signatory.
    /// </summary>
    /// <param name="floodApplicationSignatory"></param>
    /// <returns></returns>
    public async Task<FloodApplicationSignatoryEntity> SaveAsync(FloodApplicationSignatoryEntity floodApplicationSignatory)
    {
        if (floodApplicationSignatory.Id > 0)
            return await UpdateAsync(floodApplicationSignatory);
        else
            return await CreateAsync(floodApplicationSignatory);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodApplicationSignatory"></param>
    /// <returns></returns>
    private async Task<FloodApplicationSignatoryEntity> CreateAsync(FloodApplicationSignatoryEntity floodApplicationSignatory)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationSignatorySqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = floodApplicationSignatory.ApplicationId,
                @p_Designation = floodApplicationSignatory.Designation,
                @p_Title = floodApplicationSignatory.Title,
                @p_SignedOn = floodApplicationSignatory.SignedOn,
                @p_LastUpdatedBy = floodApplicationSignatory.LastUpdatedBy
            });

        floodApplicationSignatory.Id = id;

        return floodApplicationSignatory;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="floodApplicationSignatory"></param>
    /// <returns></returns>
    private async Task<FloodApplicationSignatoryEntity> UpdateAsync(FloodApplicationSignatoryEntity floodApplicationSignatory)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationSignatorySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodApplicationSignatory.Id,
                @p_ApplicationId = floodApplicationSignatory.ApplicationId,
                @p_Designation = floodApplicationSignatory.Designation,
                @p_Title = floodApplicationSignatory.Title,
                @p_SignedOn = floodApplicationSignatory.SignedOn,
                @p_LastUpdatedBy = floodApplicationSignatory.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return floodApplicationSignatory;
    }

}
