namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ParcelSurveyRepository : IParcelSurveyRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ParcelSurveyRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<FloodParcelSurveyEntity> GetSurveyAsync(int applicationId, string pamsPin)
    {
        FloodParcelSurveyEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelSurveySqlCommand();
        var results = await conn.QueryAsync<FloodParcelSurveyEntity>(sqlCommand.ToString(),
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
    public async Task<FloodParcelSurveyEntity> SaveAsync(FloodParcelSurveyEntity parcelSurvey)
    {
        if (parcelSurvey.Id > 0)
            return await UpdateAsync(parcelSurvey);
        else
            return await CreateAsync(parcelSurvey);
    }

    private async Task<FloodParcelSurveyEntity> CreateAsync(FloodParcelSurveyEntity parcelSurvey)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelSurveySqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = parcelSurvey.ApplicationId,
                @p_PamsPin = parcelSurvey.PamsPin,
                @p_Surveyor = parcelSurvey.Surveyor,
                @p_SurveyDate = parcelSurvey.SurveyDate,
                @p_LastRevision = parcelSurvey.LastRevision,
                @p_DateCorrected = parcelSurvey.DateCorrected,
                @p_LastUpdatedBy = parcelSurvey.LastUpdatedBy
            });

        parcelSurvey.Id = id;

        return parcelSurvey;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="parcelSurvey"></param>
    /// <returns></returns>
    private async Task<FloodParcelSurveyEntity> UpdateAsync(FloodParcelSurveyEntity parcelSurvey)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateParcelSurveySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = parcelSurvey.Id,
                @p_ApplicationId = parcelSurvey.ApplicationId,
                @p_PamsPin = parcelSurvey.PamsPin,
                @p_Surveyor = parcelSurvey.Surveyor,
                @p_SurveyDate = parcelSurvey.SurveyDate,
                @p_LastRevision = parcelSurvey.LastRevision,
                @p_DateCorrected = parcelSurvey.DateCorrected,
                @p_LastUpdatedBy = parcelSurvey.LastUpdatedBy
            });

        return parcelSurvey;
    }

  
}

