using OneOf.Types;
using System;

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

    public async Task SaveParcelsAsync(List<FloodParcelEntity> parcels)
    {
        using var conn = context.CreateConnection();
        foreach (var parcel in parcels)
        {
            var streetNo = parcel.PropertyAddress.Split(" ")[0];
            if (parcel.Id > 0)
            {
                var sqlCommand = new UpdateParcelSqlCommand();
                await conn.ExecuteAsync(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_Id = parcel.Id,
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
            else
            {
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

    public async Task<FloodParcelEntity> GetParcelAsync(int applicationId, string pamsPin)
    {
        FloodParcelEntity result = default;
        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelSqlCommand();
        var results = await conn.QueryAsync<FloodParcelEntity>(sqlCommand.ToString(),
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

    public async Task<List<FloodParcelStatusLogEntity>> GetParcelStatusLogAsync(int applicationId, string pamsPin)
    {
        List<FloodParcelStatusLogEntity> results = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelStatusLogSqlCommand();
        results = (await conn.QueryAsync<FloodParcelStatusLogEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_ApplicationId = applicationId,
                        @p_PamsPin = pamsPin
                    })).ToList();

        return results ?? new();
    }

    public async Task<FloodParcelEntity> UpdateAsync(FloodParcelEntity parcel)
    {
        using var conn = context.CreateConnection();
        var sqlCommand = new UpdateParcelSqlCommand(true);
        await conn.ExecuteAsync(sqlCommand.ToString(),
            commandType: CommandType.Text,
            commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
            param: new
            {
                @p_Id = parcel.Id,
                @p_PamsPin = parcel.PamsPin,
                @p_AgencyId = parcel.AgencyId,
                @p_Block = parcel.Block,
                @p_Lot = parcel.Lot,
                @p_QualificationCode = parcel.QCode,
                @p_Latitude = parcel.Latitude,
                @p_Longitude = parcel.Longitude,
                @p_StreetNo = parcel.StreetNo,
                @p_StreetAddress = parcel.StreetAddress,
                @p_Acreage = parcel.Acreage,
                @p_OwnersName = parcel.LandOwner,
                @p_OwnersAddress1 = parcel.OwnersAddress1,
                @p_OwnersAddress2 = parcel.OwnersAddress2,
                @p_OwnersCity = parcel.OwnersCity,
                @p_OwnersState = parcel.OwnersState,
                @p_OwnersZipcode = parcel.OwnersZipcode,
                @p_SquareFootage = parcel.SquareFootage,
                @p_YearOfConstruction = parcel.YearOfConstruction,
                @p_TotalAssessedValue = parcel.TotalAssessedValue,
                @p_LandValue = parcel.LandValue,
                @p_ImprovementValue = parcel.ImprovementValue,
                @p_AnnualTaxes = parcel.AnnualTaxes
            });
        return parcel;
    }

    public async Task<List<FloodParcelListEntity>> GetParcelListAsync()
    {

        List<FloodParcelListEntity> results = default;

        using var conn = context.CreateConnection();
        var sqlCommand = new GetParcelListSqlCommand();
        results = (await conn.QueryAsync<FloodParcelListEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds)).ToList();

        return results ?? new();
    }

    public async Task<FloodProgramManagerParcelsEntity> GetProgramManagerParcelsAsync(int pageNumber, int pageRows, string searchText)
    {
        FloodProgramManagerParcelsEntity result = new();

        using var conn = context.CreateConnection();
        var sqlCommand = new GetProgramManagerParcelsSqlCommand();
        var results = (await conn.QueryAsync<FloodParcelEntity>(sqlCommand.ToString(),
                    commandType: CommandType.Text,
                    commandTimeout: systemParamConfig.SQLCommandTimeoutInSeconds,
                    param: new
                    {
                        @p_PageNumber = pageNumber,
                        @p_PageRows = pageRows,
                        @p_SearchText = string.IsNullOrWhiteSpace(searchText) ? string.Empty : string.Format("%{0}%", searchText)
                    })).ToList();
        
        foreach(var item in results)
        {
            if(item.Id == 0)
            {
                result.StartNo = item.StartNo;
                result.EndNo = item.EndNo;
                result.TotalNo = item.TotalNo;
            }
            else
            {
                result.Parcels.Add(item);
            }
        }

        return result ?? new();
    }
}
