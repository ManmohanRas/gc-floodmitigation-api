using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommandstails;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

public class ParcelPropertyRepository : IParcelPropertyRepository
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
    public ParcelPropertyRepository(PresTrustSqlDbContext context, IOptions<SystemParameterConfiguration> systemParamConfigOptions)
    {
        this.context = context;
        systemParamConfig = systemParamConfigOptions.Value;
    }

    #endregion


    /// <summary>
    ///  Procedure to fetch tech details by Id.
    /// </summary>
    /// <param name="applicationId"> Id.</param>
    /// <returns> Returns floodParcelPropertyDetails Entity.</returns>
    public async Task<FloodParcelPropertyEntity> GetAsync(int applicationId, string pamsPin)
    {
        FloodParcelPropertyEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelPropertySqlCommand();
        var results = await conn.QueryAsync<FloodParcelPropertyEntity>(sqlCommand.ToString(),
                            commandType: CommandType.Text,
                            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                            param: new
                            {
                                @p_ApplicationId = applicationId,
                                @p_pamsPin = pamsPin
                            });

        result = results.FirstOrDefault();

        return result ?? new();
    }

    /// <summary>
    /// Save .
    /// </summary>
    /// <param name="floodParcelPropertyDetails"></param>
    /// <returns></returns>
    public async Task<FloodParcelPropertyEntity> SaveParcelPropertyAsync(FloodParcelPropertyEntity floodParcelPropertyDetails)
    {
        if (floodParcelPropertyDetails.Id > 0)
            return await UpdateAsync(floodParcelPropertyDetails);
        else
            return await CreateAsync(floodParcelPropertyDetails);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="floodParcelPropertyDetails"></param>
    /// <returns></returns>
    private async Task<FloodParcelPropertyEntity> CreateAsync(FloodParcelPropertyEntity floodParcelPropertyDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new CreateParcelPropertySqlCommand();
        var id = await conn.ExecuteScalarAsync<int>(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodParcelPropertyDetails.Id,
                @p_ApplicationId = floodParcelPropertyDetails.ApplicationId,
                @p_PamsPin = floodParcelPropertyDetails.PamsPin,
                @p_Priority = floodParcelPropertyDetails.Priority,
                @p_ValueEstimate = floodParcelPropertyDetails.ValueEstimate,
                @p_EstimatedPurchasePrice = floodParcelPropertyDetails.EstimatedPurchasePrice,
                @p_IsPreIrenePropertyOwner = floodParcelPropertyDetails.IsPreIrenePropertyOwner,
                @p_BRV = floodParcelPropertyDetails.BRV,
                @p_NfipPolicyNo = floodParcelPropertyDetails.NfipPolicyNo,
                @p_SourceOfValueEstimate = floodParcelPropertyDetails.SourceOfValueEstimate,
                @p_FirstFloorElevation = floodParcelPropertyDetails.FirstFloorElevation,
                @p_StructureType = floodParcelPropertyDetails.StructureType,
                @p_FoundationType = floodParcelPropertyDetails.FoundationType,
                @p_OccupancyClass = floodParcelPropertyDetails.OccupancyClass,
                @p_PercentageOfDamage = floodParcelPropertyDetails.PercentageOfDamage,
                @p_HasContaminants = floodParcelPropertyDetails.HasContaminants,
                @p_IsLowIncomeHousing = floodParcelPropertyDetails.IsLowIncomeHousing,
                @p_HasHistoricSignificance = floodParcelPropertyDetails.HasHistoricSignificance,
                @p_IsRentalProperty = floodParcelPropertyDetails.IsRentalProperty,
                @p_RentPerMonth = floodParcelPropertyDetails.RentPerMonth,
                @p_NeedSoftCost = floodParcelPropertyDetails.NeedSoftCost,
                @p_LastUpdatedBy = floodParcelPropertyDetails.LastUpdatedBy

            });

        floodParcelPropertyDetails.Id = id;

        return floodParcelPropertyDetails;
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="floodParcelPropertyDetails"></param>
    /// <returns></returns>
    private async Task<FloodParcelPropertyEntity> UpdateAsync(FloodParcelPropertyEntity floodParcelPropertyDetails)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateParcelPropertySqlCommand();
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = floodParcelPropertyDetails.Id,
                @p_ApplicationId = floodParcelPropertyDetails.ApplicationId,
                @p_PamsPin = floodParcelPropertyDetails.PamsPin,
                @p_Priority = floodParcelPropertyDetails.Priority,
                @p_ValueEstimate = floodParcelPropertyDetails.ValueEstimate,
                @p_EstimatedPurchasePrice = floodParcelPropertyDetails.EstimatedPurchasePrice,
                @p_IsPreIrenePropertyOwner = floodParcelPropertyDetails.IsPreIrenePropertyOwner,
                @p_BRV = floodParcelPropertyDetails.BRV,
                @p_NfipPolicyNo = floodParcelPropertyDetails.NfipPolicyNo,
                @p_SourceOfValueEstimate = floodParcelPropertyDetails.SourceOfValueEstimate,
                @p_FirstFloorElevation = floodParcelPropertyDetails.FirstFloorElevation,
                @p_StructureType = floodParcelPropertyDetails.StructureType,
                @p_FoundationType = floodParcelPropertyDetails.FoundationType,
                @p_OccupancyClass = floodParcelPropertyDetails.OccupancyClass,
                @p_PercentageOfDamage = floodParcelPropertyDetails.PercentageOfDamage,
                @p_HasContaminants = floodParcelPropertyDetails.HasContaminants,
                @p_IsLowIncomeHousing = floodParcelPropertyDetails.IsLowIncomeHousing,
                @p_HasHistoricSignificance = floodParcelPropertyDetails.HasHistoricSignificance,
                @p_IsRentalProperty = floodParcelPropertyDetails.IsRentalProperty,
                @p_RentPerMonth = floodParcelPropertyDetails.RentPerMonth,
                @p_NeedSoftCost = floodParcelPropertyDetails.NeedSoftCost,
                @p_LastUpdatedBy = floodParcelPropertyDetails.LastUpdatedBy,
                @p_LastUpdatedOn = DateTime.Now
            });

        return floodParcelPropertyDetails;
    }
}