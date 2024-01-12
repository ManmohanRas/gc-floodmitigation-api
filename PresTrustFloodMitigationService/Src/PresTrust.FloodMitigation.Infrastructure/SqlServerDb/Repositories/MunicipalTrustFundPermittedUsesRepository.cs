namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class MunicipalTrustFundPermittedUsesRepository: IMunicipalTrustFundPermittedUsesRepository
{
    #region " Members ... "

    private readonly PresTrustSqlDbContext context;
    protected readonly SystemParameterConfiguration systemParamConfig;

    #endregion

    #region " ctor ... "
    public MunicipalTrustFundPermittedUsesRepository(
       PresTrustSqlDbContext context,
       IOptions<SystemParameterConfiguration> systemParamConfigOptions
       )
    {
        this.context = context;
        this.systemParamConfig = systemParamConfigOptions.Value;
    }
    #endregion

    public async Task<FloodMunicipalTrustFundPermittedUsesEntity> GetMunicipalTrustFundPermittedUses(int agencyId)
    {
        FloodMunicipalTrustFundPermittedUsesEntity result = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new GetMunicipalTrustFundPermittedUsesDetailsSqlCommand();
        var results = await conn.QueryAsync<FloodMunicipalTrustFundPermittedUsesEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new { @p_AgencyId = agencyId });
        result = results.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Save floodMunicipalTrustFundPermittedUses.
    /// </summary>
    /// <param name="floodMunicipalTrustFundPermittedUses"></param>
    /// <returns></returns>
    public async Task<int> SaveAsync(FloodMunicipalTrustFundPermittedUsesEntity floodMunicipalTrustFundPermittedUses)
    {
        if (floodMunicipalTrustFundPermittedUses == null) throw new ArgumentNullException();

        if (floodMunicipalTrustFundPermittedUses.Id > 0)
            return await UpdateAsync(floodMunicipalTrustFundPermittedUses);
        else
            return await CreateAsync(floodMunicipalTrustFundPermittedUses);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodMunicipalTrustFundPermittedUses"></param>
    /// <returns></returns>
    private async Task<int> CreateAsync(FloodMunicipalTrustFundPermittedUsesEntity floodMunicipalTrustFundPermittedUses)
    {
        int result = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateMunicipalTrustFundPermittedUsesSqlCommand();
        result = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_AgencyId = floodMunicipalTrustFundPermittedUses.AgencyId,
                @p_YearOfInception = floodMunicipalTrustFundPermittedUses.YearOfInception,
                @p_AcquisitionOfLands = floodMunicipalTrustFundPermittedUses.AcquisitionOfLands,
                @p_AcquisitionOfFarmLands = floodMunicipalTrustFundPermittedUses.AcquisitionOfFarmLands,
                @p_DevelopmentOfLands = floodMunicipalTrustFundPermittedUses.DevelopmentOfLands,
                @p_MaintenanceOfLands = floodMunicipalTrustFundPermittedUses.MaintenanceOfLands,
                @p_SalariesAndBenefits = floodMunicipalTrustFundPermittedUses.SalariesAndBenefits,
                @p_BondDownPayments = floodMunicipalTrustFundPermittedUses.BondDownPayments,
                @p_HistoricPreservation = floodMunicipalTrustFundPermittedUses.HistoricPreservation,
                @p_OpenspaceMasterPlan = floodMunicipalTrustFundPermittedUses.OpenspaceMasterPlan,
                @p_OpenspaceMasterPlanDate = floodMunicipalTrustFundPermittedUses.OpenspaceMasterPlanDate,
                @p_GreenAcresGrant = floodMunicipalTrustFundPermittedUses.GreenAcresGrant,
                @p_Other = floodMunicipalTrustFundPermittedUses.Other,
                @p_TrustFundComments = floodMunicipalTrustFundPermittedUses.TrustFundComments
            });

        return result;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodMunicipalTrustFundPermittedUses"></param>
    /// <returns></returns>
    private async Task<int> UpdateAsync(FloodMunicipalTrustFundPermittedUsesEntity floodMunicipalTrustFundPermittedUses)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateMunicipalTrustFundPermittedUsesSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodMunicipalTrustFundPermittedUses.Id,
                @p_AgencyId = floodMunicipalTrustFundPermittedUses.AgencyId,
                @p_YearOfInception = floodMunicipalTrustFundPermittedUses.YearOfInception,
                @p_AcquisitionOfLands = floodMunicipalTrustFundPermittedUses.AcquisitionOfLands,
                @p_AcquisitionOfFarmLands = floodMunicipalTrustFundPermittedUses.AcquisitionOfFarmLands,
                @p_DevelopmentOfLands = floodMunicipalTrustFundPermittedUses.DevelopmentOfLands,
                @p_MaintenanceOfLands = floodMunicipalTrustFundPermittedUses.MaintenanceOfLands,
                @p_SalariesAndBenefits = floodMunicipalTrustFundPermittedUses.SalariesAndBenefits,
                @p_BondDownPayments = floodMunicipalTrustFundPermittedUses.BondDownPayments,
                @p_HistoricPreservation = floodMunicipalTrustFundPermittedUses.HistoricPreservation,
                @p_OpenspaceMasterPlan = floodMunicipalTrustFundPermittedUses.OpenspaceMasterPlan,
                @p_OpenspaceMasterPlanDate = floodMunicipalTrustFundPermittedUses.OpenspaceMasterPlanDate,
                @p_GreenAcresGrant = floodMunicipalTrustFundPermittedUses.GreenAcresGrant,
                @p_Other = floodMunicipalTrustFundPermittedUses.Other,
                @p_TrustFundComments = floodMunicipalTrustFundPermittedUses.TrustFundComments
            });

        return floodMunicipalTrustFundPermittedUses.Id;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="yearOfInception"></param>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    public async Task UpdateYearOfInception(string yearOfInception, int agencyId)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateYearOfInceptionSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_YearOfInception = yearOfInception,
                @p_AgencyId = agencyId
            });

    }

}
