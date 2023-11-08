using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommandstails;
using System;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
public class TechDetailsRepository : ITechDetailsRepository
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
    public TechDetailsRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion


    /// <summary>
    ///  Procedure to fetch tech details by Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns FloodTechDetails Entity.</returns>
    public async Task<FloodTechDetailsEntity> GetTechAsync(int applicationId, string pamsPin)
    {
        FloodTechDetailsEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetTechDetailsSqlCommand();
        var results = await conn.QueryAsync<FloodTechDetailsEntity>(sqlCommand.ToString(),
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

    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodTechDetails"></param>
    /// <returns></returns>
    public async Task<FloodTechDetailsEntity> SaveTechAsync(FloodTechDetailsEntity floodTechDetails)
    {
        if (floodTechDetails.Id > 0)
            return await UpdateAsync(floodTechDetails);
        else
            return await SaveAsync(floodTechDetails);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodTechDetails"></param>
    /// <returns></returns>
    private async Task<FloodTechDetailsEntity> SaveAsync(FloodTechDetailsEntity floodTechDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreateTechDetailsSqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_ApplicationId = floodTechDetails.ApplicationId,
                @p_PamsPin = floodTechDetails.PamsPin,
                @p_FEMASevereRepetitiveLossList = floodTechDetails.FEMASevereRepetitiveLossList,
                @p_FEMARepetitiveLossList = floodTechDetails.FEMARepetitiveLossList,
                @p_IsthepropertywithinthePassaicRiverBasin = floodTechDetails.IsthepropertywithinthePassaicRiverBasin,
                @p_IsthepropertywithinFloodway = floodTechDetails.IsthepropertywithinFloodway,
                @p_IsthepropertywithinFloodplain = floodTechDetails.IsthepropertywithinFloodplain,
                @p_Claim10Years = floodTechDetails.Claim10Years,
                @p_TotalOfClaims = floodTechDetails.TotalOfClaims,
                @p_BenefitCostRatio = floodTechDetails.BenefitCostRatio,
                @p_FEMACommunityId = floodTechDetails.FEMACommunityId,
                @p_FirmEffectiveDate = floodTechDetails.FirmEffectiveDate,
                @p_FirmPanel = floodTechDetails.FirmPanel,
                @p_FirmPanelFinal = floodTechDetails.FirmPanelFinal,
                @p_FloodZoneDesignation = floodTechDetails.FloodZoneDesignation,
                @p_BaseFloodElevation = floodTechDetails.BaseFloodElevation,    
                @p_BaseFloodElevationFinal = floodTechDetails.BaseFloodElevationFinal,
                @p_RiverId = floodTechDetails.RiverId,
                @p_RiverIdFinal = floodTechDetails.RiverIdFinal,    
                @p_FisEffectiveDate = floodTechDetails.FisEffectiveDate,
                @p_FloodProfile = floodTechDetails.FloodProfile,    
                @p_FloodProfileFinal = floodTechDetails.FloodProfileFinal,
                @p_FloodSource = floodTechDetails.FloodSource,
                @p_FirstFloodElevation = floodTechDetails.FirstFloodElevation,
                @p_FirstFloodElevationFinal = floodTechDetails.FirstFloodElevationFinal,
                @p_StreambedElevation = floodTechDetails.StreambedElevation,
                @p_StreambedElevationFinal = floodTechDetails.StreambedElevationFinal,
                @p_ElevationBeforeMitigation = floodTechDetails.ElevationBeforeMitigation,
                @p_ElevationBeforeMitigationFinal = floodTechDetails.ElevationBeforeMitigationFinal,
                @p_FloodType = floodTechDetails.FloodType,
                @p_TenPercent = floodTechDetails.TenPercent,
                @p_TwoPercent = floodTechDetails.TwoPercent,
                @p_OnePercent = floodTechDetails.OnePercent,
                @p_PointOnePercent = floodTechDetails.PointOnePercent,
                @p_LastUpdatedBy = floodTechDetails.LastUpdatedBy
            });

        floodTechDetails.Id = id;

        return floodTechDetails;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="floodTechDetails"></param>
    /// <returns></returns>
    private async Task<FloodTechDetailsEntity> UpdateAsync(FloodTechDetailsEntity floodTechDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateTechDetailsSqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodTechDetails.Id,
                @p_ApplicationId = floodTechDetails.ApplicationId,
                @p_PamsPin = floodTechDetails.PamsPin,
                @p_FEMASevereRepetitiveLossList = floodTechDetails.FEMASevereRepetitiveLossList,
                @p_FEMARepetitiveLossList = floodTechDetails.FEMARepetitiveLossList,
                @p_IsthepropertywithinthePassaicRiverBasin = floodTechDetails.IsthepropertywithinthePassaicRiverBasin,
                @p_IsthepropertywithinFloodway = floodTechDetails.IsthepropertywithinFloodway,
                @p_IsthepropertywithinFloodplain = floodTechDetails.IsthepropertywithinFloodplain,
                @p_Claim10Years = floodTechDetails.Claim10Years,
                @p_TotalOfClaims = floodTechDetails.TotalOfClaims,
                @p_BenefitCostRatio = floodTechDetails.BenefitCostRatio,
                @p_FEMACommunityId = floodTechDetails.FEMACommunityId,
                @p_FirmEffectiveDate = floodTechDetails.FirmEffectiveDate,
                @p_FirmPanel = floodTechDetails.FirmPanel,
                @p_FirmPanelFinal = floodTechDetails.FirmPanelFinal,
                @p_FloodZoneDesignation = floodTechDetails.FloodZoneDesignation,
                @p_BaseFloodElevation = floodTechDetails.BaseFloodElevation,
                @p_BaseFloodElevationFinal = floodTechDetails.BaseFloodElevationFinal,
                @p_RiverId = floodTechDetails.RiverId,
                @p_RiverIdFinal = floodTechDetails.RiverIdFinal,
                @p_FisEffectiveDate = floodTechDetails.FisEffectiveDate,
                @p_FloodProfile = floodTechDetails.FloodProfile,
                @p_FloodProfileFinal = floodTechDetails.FloodProfileFinal,
                @p_FloodSource = floodTechDetails.FloodSource,
                @p_FirstFloodElevation = floodTechDetails.FirstFloodElevation,
                @p_FirstFloodElevationFinal = floodTechDetails.FirstFloodElevationFinal,
                @p_StreambedElevation = floodTechDetails.StreambedElevation,
                @p_StreambedElevationFinal = floodTechDetails.StreambedElevationFinal,
                @p_ElevationBeforeMitigation = floodTechDetails.ElevationBeforeMitigation,
                @p_ElevationBeforeMitigationFinal = floodTechDetails.ElevationBeforeMitigationFinal,
                @p_FloodType = floodTechDetails.FloodType,
                @p_TenPercent = floodTechDetails.TenPercent,
                @p_TwoPercent = floodTechDetails.TwoPercent,
                @p_OnePercent = floodTechDetails.OnePercent,
                @p_PointOnePercent = floodTechDetails.PointOnePercent,
                @p_LastUpdatedBy = floodTechDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return floodTechDetails;
    }
}

