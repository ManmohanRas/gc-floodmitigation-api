namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ParcelFinanceRepository: IParcelFinanceRepository
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
    public ParcelFinanceRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion

    /// <summary>
    /// Procedure to fetch parcels Finance
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    public async Task<FloodParcelFinanceEntity> GetParceFinanceAsync(int applicationId, string pamsPin)
    {
        FloodParcelFinanceEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelFinanceSqlCommand();
        var results = await conn.QueryAsync<FloodParcelFinanceEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new {
                                @p_ApplicationId = applicationId,
                                @p_pamsPin = pamsPin
                            });

        result = results.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Save Parcel Finance.
    /// </summary>
    /// <param name="parcelFinance"></param>
    /// <returns></returns>
    public async Task<FloodParcelFinanceEntity> SaveAsync(FloodParcelFinanceEntity parcelFinance)
    {
        if (parcelFinance.Id > 0)
            return await UpdateAsync(parcelFinance);
        else
            return await CreateAsync(parcelFinance);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="parcelFinance"></param>
    /// <returns></returns>
    private async Task<FloodParcelFinanceEntity> CreateAsync(FloodParcelFinanceEntity parcelFinance)
    {
        int id = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelFinanceSqlCommad();
        id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = parcelFinance.ApplicationId,
                @p_PamsPin = parcelFinance.PamsPin,
                @p_EstimatePurchasePrice = parcelFinance.EstimatePurchasePrice,
                @p_AdditionalSoftCostEstimate = parcelFinance.AdditionalSoftCostEstimate,
                @p_AppraisedValue = parcelFinance.AppraisedValue,
                @p_AMV = parcelFinance.AMV,
                @p_TotalFEMABenifits = parcelFinance.TotalFEMABenifits,
                @p_DOBAffidavitType = parcelFinance.DOBAffidavitType,
                @p_DOBAffidavitAmt = parcelFinance.DOBAffidavitAmt,
                @p_SoftCostFMPAmt = parcelFinance.SoftCostFMPAmt,
                @p_HardCostFMPAmt = parcelFinance.HardCostFMPAmt,
                @p_AppraisersFee = parcelFinance.AppraisersFee,
                @p_SurveyorsFee = parcelFinance.SurveyorsFee,
                @p_LastUpdatedBy = parcelFinance.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        parcelFinance.Id = id;

        return parcelFinance;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="parcelFinance"></param>
    /// <returns></returns>
    private async Task<FloodParcelFinanceEntity> UpdateAsync(FloodParcelFinanceEntity parcelFinance)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateParcelFinanceSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = parcelFinance.Id,
                @p_ApplicationId = parcelFinance.ApplicationId,
                @p_PamsPin = parcelFinance.PamsPin,
                @p_EstimatePurchasePrice = parcelFinance.EstimatePurchasePrice,
                @p_AdditionalSoftCostEstimate = parcelFinance.AdditionalSoftCostEstimate,
                @p_AppraisedValue = parcelFinance.AppraisedValue,
                @p_AMV = parcelFinance.AMV,
                @p_TotalFEMABenifits = parcelFinance.TotalFEMABenifits,
                @p_DOBAffidavitType = parcelFinance.DOBAffidavitType,
                @p_DOBAffidavitAmt = parcelFinance.DOBAffidavitAmt,
                @p_SoftCostFMPAmt = parcelFinance.SoftCostFMPAmt,
                @p_HardCostFMPAmt = parcelFinance.HardCostFMPAmt,
                @p_AppraisersFee = parcelFinance.AppraisersFee,
                @p_SurveyorsFee = parcelFinance.SurveyorsFee,
                @p_LastUpdatedBy = parcelFinance.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return parcelFinance;
    }
}
