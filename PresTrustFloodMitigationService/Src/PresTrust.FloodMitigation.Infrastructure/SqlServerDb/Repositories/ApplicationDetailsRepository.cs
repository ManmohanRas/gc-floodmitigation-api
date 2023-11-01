namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public class ApplicationDetailsRepository : IApplicationDetailsRepository
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
    public ApplicationDetailsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion
    public async Task<FloodApplicationAdminDetailsEntity> GetAsync(int applicationId)
    {
        FloodApplicationAdminDetailsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetApplicationAdminDetailsSqlCommand();
        var results = await conn.QueryAsync<FloodApplicationAdminDetailsEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                            });


        result = results.FirstOrDefault();

        return result;
    }
    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    public async Task<FloodApplicationAdminDetailsEntity> SaveAsync(FloodApplicationAdminDetailsEntity floodAppDetails)
    {
        if (floodAppDetails.Id > 0)
            return await UpdateAsync(floodAppDetails);
        else
            return await CreateAsync(floodAppDetails);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    private async Task<FloodApplicationAdminDetailsEntity> CreateAsync(FloodApplicationAdminDetailsEntity floodAppDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreateApplicationAdminDetailsSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_ApplicationId = floodAppDetails.ApplicationId,
                @P_MunicipalResolutionDate = floodAppDetails.MunicipalResolutionDate,
                @P_MunicipalResolutionNumber = floodAppDetails.MunicipalResolutionNumber,
                @P_FmcPreliminaryApprovalDate = floodAppDetails.FMCPreliminaryApprovalDate,
                @P_FMCPreliminaryNumber = floodAppDetails.FMCPreliminaryNumber,
                @P_BccPreliminaryApprovalDate = floodAppDetails.BCCPreliminaryApprovalDate,
                @P_BccPreliminaryNumber = floodAppDetails.BCCPreliminaryNumber,
                @P_ProjectDescription = floodAppDetails.ProjectDescription,
                @P_FundingExpirationDate = floodAppDetails.FundingExpirationDate,
                @P_FirstFundingExpirationDate = floodAppDetails.FirstFundingExpirationDate,
                @P_SecondFundingExpirationDate = floodAppDetails.SecondFundingExpirationDate,
                @P_CommissionerMeetingDate = floodAppDetails.CommissionerMeetingDate,
                @P_FirstCommitteeMeetingDate = floodAppDetails.FirstCommitteeMeetingDate,
                @P_SecondCommitteeMeetingDate = floodAppDetails.SecondCommitteeMeetingDate,
                @p_LastUpdatedBy = floodAppDetails.LastUpdatedBy

            });
        floodAppDetails.Id = id;

        return floodAppDetails;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodAppDetails"></param>
    /// <returns></returns>
    private async Task<FloodApplicationAdminDetailsEntity> UpdateAsync(FloodApplicationAdminDetailsEntity floodAppDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateApplicationAdminDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @P_Id = floodAppDetails.Id,
                @P_ApplicationId = floodAppDetails.ApplicationId,
                @P_MunicipalResolutionDate = floodAppDetails.MunicipalResolutionDate,
                @P_MunicipalResolutionNumber = floodAppDetails.MunicipalResolutionNumber,
                @P_FmcPreliminaryApprovalDate = floodAppDetails.FMCPreliminaryApprovalDate,
                @P_FMCPreliminaryNumber = floodAppDetails.FMCPreliminaryNumber,
                @P_BccPreliminaryApprovalDate = floodAppDetails.BCCPreliminaryApprovalDate,
                @P_BccPreliminaryNumber = floodAppDetails.BCCPreliminaryNumber,
                @P_ProjectDescription = floodAppDetails.ProjectDescription,
                @P_FundingExpirationDate = floodAppDetails.FundingExpirationDate,
                @P_FirstFundingExpirationDate = floodAppDetails.FirstFundingExpirationDate,
                @P_SecondFundingExpirationDate = floodAppDetails.SecondFundingExpirationDate,
                @P_CommissionerMeetingDate = floodAppDetails.CommissionerMeetingDate,
                @P_FirstCommitteeMeetingDate = floodAppDetails.FirstCommitteeMeetingDate,
                @P_SecondCommitteeMeetingDate = floodAppDetails.SecondCommitteeMeetingDate,
                @p_LastUpdatedBy = floodAppDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });
        return floodAppDetails;
    }

}
