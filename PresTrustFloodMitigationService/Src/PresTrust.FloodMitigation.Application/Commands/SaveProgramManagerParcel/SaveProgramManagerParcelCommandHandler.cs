using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PresTrust.FloodMitigation.Application.Commands;


public class SaveProgramManagerParcelCommandHandler : BaseHandler, IRequestHandler<SaveProgramManagerParcelCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelRepository repoParcel;
    private readonly IParcelPropertyRepository repoParcelProperty;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationParcelRepository repoAppParcel;

    public SaveProgramManagerParcelCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IParcelRepository repoParcel,
        IParcelPropertyRepository repoParcelProperty,
        IApplicationParcelRepository repoAppParcel,
        IPropertyBrokenRuleRepository repoBrokenRules
    ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoParcel = repoParcel;
        this.repoParcelProperty = repoParcelProperty;
        this.repoBrokenRules = repoBrokenRules;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SaveProgramManagerParcelCommand request, CancellationToken cancellationToken)
    {
        FloodParcelEntity reqParcel = new();
        reqParcel = mapper.Map<SaveProgramManagerParcelCommand, FloodParcelEntity>(request);
        reqParcel.PamsPin = string.Format("{0}_{1}_{2}_{3}", reqParcel.AgencyId, reqParcel.Block, reqParcel.Lot, reqParcel.QCode);
        if (reqParcel.PamsPin.EndsWith('_'))
        {
            reqParcel.PamsPin = reqParcel.PamsPin.Substring(0, reqParcel.PamsPin.Length - 1);
        }
        var parcel = await repoParcel.GetProgramManagerParcelAsync(reqParcel.Id);
        if (parcel != null)
        {
            parcel.PamsPin = reqParcel.PamsPin;
            parcel.IsElevated = reqParcel.IsElevated;
            parcel.StreetNo = reqParcel.StreetNo;
            parcel.StreetAddress = reqParcel.StreetAddress;
            parcel.Block = reqParcel.Block;
            parcel.Lot = reqParcel.Lot;
            parcel.QCode = reqParcel.QCode;
            parcel.Latitude = reqParcel.Latitude;
            parcel.Longitude = reqParcel.Longitude;
            parcel.Acreage = reqParcel.Acreage;
            parcel.YearOfConstruction = reqParcel.YearOfConstruction;
            parcel.SquareFootage = reqParcel.SquareFootage;
            parcel.LandOwner = reqParcel.LandOwner;
            parcel.OwnersAddress1 = reqParcel.OwnersAddress1;
            parcel.OwnersAddress2 = reqParcel.OwnersAddress2;
            parcel.OwnersCity = reqParcel.OwnersCity;
            parcel.OwnersState = reqParcel.OwnersState;
            parcel.OwnersZipcode = reqParcel.OwnersZipcode;
            parcel.TotalAssessedValue = reqParcel.TotalAssessedValue;
            parcel.LandValue = reqParcel.LandValue;
            parcel.ImprovementValue = reqParcel.ImprovementValue;
            parcel.AnnualTaxes = reqParcel.AnnualTaxes;
            parcel = await repoParcel.SaveProgramManagerParcelAsync(parcel);
        }
        return reqParcel.Id;
    }
}
