namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class SignatoryRepository : ISignatoryRepository
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
    public SignatoryRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion



    /// <summary>
    ///  Procedure to fetch Signatory details by Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns FloodSignatory Entity.</returns>
    public async Task<FloodSignatoryEntity> GetSignatoryAsync(int applicationId)
        {
            FloodSignatoryEntity result = default;
            using var conn = context.CreateConnection();
            var sqlCommand = new GetSignatorySqlCommand();
            var results = await conn.QueryAsync<FloodSignatoryEntity>(sqlCommand.ToString(),
                                commandType: CommandType.Text,
                                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                                param: new { 
                                    @p_ApplicationId = applicationId
                                   });

            result = results.FirstOrDefault();

            return result;
    }

    /// <summary>
    /// Save Signatory.
    /// </summary>
    /// <param name="floodSignatory"></param>
    /// <returns></returns>
    public async Task<FloodSignatoryEntity> SaveAsync(FloodSignatoryEntity floodSignatory)
    {
        if (floodSignatory.Id > 0)
            return await UpdateAsync(floodSignatory);
        else
            return await CreateAsync(floodSignatory);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodSignatory"></param>
    /// <returns></returns>
    private async Task<FloodSignatoryEntity> CreateAsync(FloodSignatoryEntity floodSignatory)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateSignatorySqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = floodSignatory.ApplicationId,
                @p_Designation = floodSignatory.Designation,
                @p_Title = floodSignatory.Title,
                @p_SignedOn = floodSignatory.SignedOn,
                @p_LastUpdatedBy = floodSignatory.LastUpdatedBy
            });

        floodSignatory.Id = id;

        return floodSignatory;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="floodSignatory"></param>
    /// <returns></returns>
    private async Task<FloodSignatoryEntity> UpdateAsync(FloodSignatoryEntity floodSignatory)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateSignatorySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodSignatory.Id,
                @p_ApplicationId = floodSignatory.ApplicationId,
                @p_Designation = floodSignatory.Designation,
                @p_Title = floodSignatory.Title,
                @p_SignedOn = floodSignatory.SignedOn,
                @p_LastUpdatedBy = floodSignatory.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return floodSignatory;
    }

}
