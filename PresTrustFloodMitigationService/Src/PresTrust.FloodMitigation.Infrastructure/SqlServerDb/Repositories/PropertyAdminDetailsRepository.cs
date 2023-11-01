namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class PropertyAdminDetailsRepository : IPropertyAdminDetailsRepository
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
    public PropertyAdminDetailsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    public async Task<FloodPropertyAdminDetailsEntity> GetAsync(int applicationId, string Pamspin)
    {
        FloodPropertyAdminDetailsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetPropertyAdminDetailsSqlCommand();
        var results = await conn.QueryAsync<FloodPropertyAdminDetailsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                                @p_Pamspin = Pamspin,
                            });

        result = results.FirstOrDefault();

        return result;
    }
    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodPropDetails"></param>
    /// <returns></returns>
    public async Task<FloodPropertyAdminDetailsEntity> SaveAsync(FloodPropertyAdminDetailsEntity floodPropDetails)
    {
        if (floodPropDetails.Id > 0)
            return await UpdateAsync(floodPropDetails);
        else
            return await CreateAsync(floodPropDetails);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    private async Task<FloodPropertyAdminDetailsEntity> CreateAsync(FloodPropertyAdminDetailsEntity floodPropDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreatePropertyAdminDetailsSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_ApplicationId = floodPropDetails.ApplicationId,
                @P_PamsPin = floodPropDetails.PamsPin,
                @P_DobDocumentsMissingDate = floodPropDetails.DobDocumentsMissingDate,
                @P_FmcFinalApprovalDate = floodPropDetails.FmcFinalApprovalDate,
                @P_FmcFinalNumber = floodPropDetails.FmcFinalNumber,
                @P_BccFinalNumber = floodPropDetails.BccFinalNumber,
                @P_BccFinalApprovalDate = floodPropDetails.BccFinalApprovalDate,
                @P_MunicipalPurchaseDate  = floodPropDetails.MunicipalPurchaseDate,
                @P_MunicipalPurchaseNumber = floodPropDetails.MunicipalPurchaseNumber,
                @P_GrantAgreementDate = floodPropDetails.GrantAgreementDate,
                @P_GrantAgreementExpirationDate = floodPropDetails.GrantAgreementExpirationDate,
                @P_DueDiligenceDocumentsMissingDate = floodPropDetails.DueDiligenceDocumentsMissingDate,
                @P_ScheduleClosingDate = floodPropDetails.ScheduleClosingDate,
                @P_SoftCostReimbursementRequestDate  = floodPropDetails.SoftCostReimbursementRequestDate,
                @P_FmcSoftcostReimbApprovalDate = floodPropDetails.FmcSoftcostReimbApprovalDate,
                @P_FmcSoftcostReimbApprovalNumber = floodPropDetails.FmcSoftcostReimbApprovalNumber,
                @P_BccSoftcostReimbApprovalDate = floodPropDetails.BccSoftcostReimbApprovalDate,
                @P_BccSoftcostReimbApprovalNumber = floodPropDetails.BccSoftcostReimbApprovalNumber,
                @P_DoesHomeOwnerHaveNFIPInsurance = floodPropDetails.DoesHomeOwnerHaveNFIPInsurance,
                @P_IsDEPInvolved = floodPropDetails.IsDEPInvolved,
                @P_IsPARRequestedbyFunder  = floodPropDetails.IsPARRequestedbyFunder,
                @P_LastUpdatedBy = floodPropDetails.LastUpdatedBy
            });
        floodPropDetails.Id = id;

        return floodPropDetails;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodPropDetails"></param>
    /// <returns></returns>
    private async Task<FloodPropertyAdminDetailsEntity> UpdateAsync(FloodPropertyAdminDetailsEntity floodPropDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdatePropertyAdminDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_Id = floodPropDetails.Id,
                @P_ApplicationId = floodPropDetails.ApplicationId,
                @P_PamsPin = floodPropDetails.PamsPin,
                @P_DobDocumentsMissingDate = floodPropDetails.DobDocumentsMissingDate,
                @P_FmcFinalApprovalDate = floodPropDetails.FmcFinalApprovalDate,
                @P_FmcFinalNumber = floodPropDetails.FmcFinalNumber,
                @P_BccFinalNumber = floodPropDetails.BccFinalNumber,
                @P_BccFinalApprovalDate = floodPropDetails.BccFinalApprovalDate,
                @P_MunicipalPurchaseDate = floodPropDetails.MunicipalPurchaseDate,
                @P_MunicipalPurchaseNumber = floodPropDetails.MunicipalPurchaseNumber,
                @P_GrantAgreementDate = floodPropDetails.GrantAgreementDate,
                @P_GrantAgreementExpirationDate = floodPropDetails.GrantAgreementExpirationDate,
                @P_DueDiligenceDocumentsMissingDate = floodPropDetails.DueDiligenceDocumentsMissingDate,
                @P_ScheduleClosingDate = floodPropDetails.ScheduleClosingDate,
                @P_SoftCostReimbursementRequestDate = floodPropDetails.SoftCostReimbursementRequestDate,
                @P_FmcSoftcostReimbApprovalDate = floodPropDetails.FmcSoftcostReimbApprovalDate,
                @P_FmcSoftcostReimbApprovalNumber = floodPropDetails.FmcSoftcostReimbApprovalNumber,
                @P_BccSoftcostReimbApprovalDate = floodPropDetails.BccSoftcostReimbApprovalDate,
                @P_BccSoftcostReimbApprovalNumber = floodPropDetails.BccSoftcostReimbApprovalNumber,
                @P_DoesHomeOwnerHaveNFIPInsurance = floodPropDetails.DoesHomeOwnerHaveNFIPInsurance,
                @P_IsDEPInvolved = floodPropDetails.IsDEPInvolved,
                @P_IsPARRequestedbyFunder = floodPropDetails.IsPARRequestedbyFunder,
                @P_LastUpdatedBy = floodPropDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });
        return floodPropDetails;
    }
}
