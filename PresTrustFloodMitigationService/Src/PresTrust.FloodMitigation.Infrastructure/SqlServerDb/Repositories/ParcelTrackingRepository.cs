namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ParcelTrackingRepository : IParcelTrackingRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;
    
    public ParcelTrackingRepository
        (
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task<FloodParcelTrackingEntity> GetTrackingAsync(int applicationId, string pamsPin)
    {
        FloodParcelTrackingEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelTrackingSqlCommand();
        var results = await conn.QueryAsync<FloodParcelTrackingEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                                @p_PamsPin = pamsPin
                            });
        result = results.FirstOrDefault();

        return result;
    }

    public async Task<FloodParcelTrackingEntity> SaveAsync(FloodParcelTrackingEntity parcelTracking)
    {
        if (parcelTracking.Id > 0)
            return await UpdateAsync(parcelTracking);
        else
            return await CreateAsync(parcelTracking);
    }

    private async Task<FloodParcelTrackingEntity> CreateAsync(FloodParcelTrackingEntity parcelTracking)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelTrackingSqlCommand();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = parcelTracking.ApplicationId,
                @p_PamsPin = parcelTracking.PamsPin,
                @p_ClosingDate = parcelTracking.ClosingDate,
                @p_DeedBook = parcelTracking.DeedBook,
                @p_DeedPage = parcelTracking.DeedPage,
                @p_DeedDate = parcelTracking.DeedDate,
                @p_DemolitionDate = parcelTracking.DemolitionDate,
                @p_SiteVisitConfirmDate = parcelTracking.SiteVisitConfirmDate,
                @p_PublicPark = parcelTracking.PublicPark,
                @p_RainGarden = parcelTracking.RainGarden,
                @p_CommunityGarden = parcelTracking.CommunityGarden,
                @p_ActiveRecreation = parcelTracking.ActiveRecreation,
                @p_NaturalHabitat = parcelTracking.NaturalHabitat,
                @p_LastUpdatedBy = parcelTracking.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        parcelTracking.Id = id;

        return parcelTracking;
    }

    private async Task<FloodParcelTrackingEntity> UpdateAsync(FloodParcelTrackingEntity parcelTracking)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateParcelTrackingSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = parcelTracking.Id,
                @p_ApplicationId = parcelTracking.ApplicationId,
                @p_PamsPin = parcelTracking.PamsPin,
                @p_ClosingDate = parcelTracking.ClosingDate,
                @p_DeedBook = parcelTracking.DeedBook,
                @p_DeedPage = parcelTracking.DeedPage,
                @p_DeedDate = parcelTracking.DeedDate,
                @p_DemolitionDate = parcelTracking.DemolitionDate,
                @p_SiteVisitConfirmDate = parcelTracking.SiteVisitConfirmDate,
                @p_PublicPark = parcelTracking.PublicPark,
                @p_RainGarden = parcelTracking.RainGarden,
                @p_CommunityGarden = parcelTracking.CommunityGarden,
                @p_ActiveRecreation = parcelTracking.ActiveRecreation,
                @p_NaturalHabitat = parcelTracking.NaturalHabitat,
                @p_LastUpdatedBy = parcelTracking.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return parcelTracking;
    }
}
