namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ParcelRepository : IParcelRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public ParcelRepository(
        PresTrustSqlDbContext context,
        IOptions<SystemParameterConfiguration> systemParamConfigOptions
        )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }

    public async Task SaveAsync(List<FloodParcelEntity> parcels)
    {
        using var conn = context.CreateConnection();
        foreach (var parcel in parcels)
        {
            var streetNo = parcel.PropertyAddress.Split(" ")[0];
            var sqlCommand = new CreateParcelSqlCommand();
            await conn.ExecuteAsync(sqlCommand.ToString(),
                commandType: CommandType.Text,
                commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                param: new
                {
                    @p_PamsPin = parcel.PamsPin,
                    @p_AgencyId = parcel.AgencyId,
                    @p_Block = parcel.Block,
                    @p_Lot = parcel.Lot,
                    @p_QualificationCode = parcel.QCode,
                    @p_StreetNo = streetNo,
                    @p_StreetAddress = parcel.PropertyAddress.Substring(streetNo.Length + 1),
                    @p_OwnersName = parcel.LandOwner
                });
        }
    }

    public async Task<FloodParcelEntity> GetParcelAsync(int applicationId, string pamsPin)
    {
        FloodParcelEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelSqlCommand();
        var results = await conn.QueryAsync<FloodParcelEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new {
                                @p_ApplicationId = applicationId,
                                @p_PamsPin = pamsPin
                            });
        result = results.FirstOrDefault();

        return result ?? new();
    }

    public async Task<List<FloodParcelStatusLogEntity>> GetParcelStatusLogAsync(int applicationId, string pamsPin)
    {
        List<FloodParcelStatusLogEntity> results = default;

        using var conn = context.CreateConnection();
        string sqlCommand = new GetParcelStatusLogSqlCommand().ToString();
        results = (await conn.QueryAsync<FloodParcelStatusLogEntity>(sqlCommand,
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = applicationId,
                        @p_PamsPin = pamsPin
                    })).ToList();

        return results ?? new();
    }
}
