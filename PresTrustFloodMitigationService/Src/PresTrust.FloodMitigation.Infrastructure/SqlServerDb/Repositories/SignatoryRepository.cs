using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class SignatoryRepository : ISignatureRepository
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
    ///  Procedure to fetch signature details by Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns HistSignature Entity.</returns>
    public async Task<FloodSignatoryEntity> GetSignatureAsync(int applicationId)
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
    /// Save Signature.
    /// </summary>
    /// <param name="histSignature"></param>
    /// <returns></returns>
    public async Task<FloodSignatoryEntity> SaveAsync(FloodSignatoryEntity floodSignature)
    {
        if (floodSignature.Id > 0)
            return await UpdateAsync(floodSignature);
        else
            return await CreateAsync(floodSignature);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="histSignature"></param>
    /// <returns></returns>
    private async Task<FloodSignatoryEntity> CreateAsync(FloodSignatoryEntity floodSignature)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateSignatorySqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = floodSignature.ApplicationId,
                @p_Designation = floodSignature.Designation,
                @p_Title = floodSignature.Title,
                @p_SignedOn = floodSignature.SignatureOn,
                @p_LastUpdatedBy = floodSignature.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        floodSignature.Id = id;

        return floodSignature;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="histSignature"></param>
    /// <returns></returns>
    private async Task<FloodSignatoryEntity> UpdateAsync(FloodSignatoryEntity floodSignature)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateSignatorySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodSignature.Id,
                @p_ApplicationId = floodSignature.ApplicationId,
                @p_Designation = floodSignature.Designation,
                @p_Title = floodSignature.Title,
                @p_SignedOn = floodSignature.SignatureOn,
                @p_LastUpdatedBy = floodSignature.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return floodSignature;
    }

}
