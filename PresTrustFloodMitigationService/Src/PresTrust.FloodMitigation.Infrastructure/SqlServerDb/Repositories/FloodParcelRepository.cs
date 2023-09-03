namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class FloodParcelRepository : IFloodParcelRepository
{
    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    public FloodParcelRepository(
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
}
