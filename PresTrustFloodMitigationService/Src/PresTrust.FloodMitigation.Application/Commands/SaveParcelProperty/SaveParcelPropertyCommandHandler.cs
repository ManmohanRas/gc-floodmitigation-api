using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PresTrust.FloodMitigation.Application.Commands;


public class SaveParcelPropertyCommandHandler : BaseHandler, IRequestHandler<SaveParcelPropertyCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelRepository repoParcel;
    private readonly IParcelPropertyRepository repoParcelProperty;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationParcelRepository repoAppParcel;

    public SaveParcelPropertyCommandHandler
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
    public async Task<int> Handle(SaveParcelPropertyCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // map command object to the FloodParcelPropertyEntity
        FloodParcelEntity reqParcel = new();
        reqParcel = mapper.Map<SaveParcelPropertyCommand, FloodParcelEntity>(request);
        FloodParcelPropertyEntity reqParcelProperty = new();
        reqParcelProperty = mapper.Map<SaveParcelPropertyCommand, FloodParcelPropertyEntity>(request);
        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(application, property, reqParcelProperty, reqParcel);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {   // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.PROPERTY, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);
            reqParcel = await repoParcel.UpdateAsync(reqParcel);
            reqParcelProperty = await repoParcelProperty.SaveParcelPropertyAsync(reqParcelProperty);
            reqParcelProperty.LastUpdatedBy = userContext.Email;
            scope.Complete();
        }
        return reqParcelProperty.Id;
    }
    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodParcelPropertyEntity reqParcelProperty, FloodParcelEntity reqFloodParcel)
    {
        int sectionId = (int)PropertySectionEnum.PROPERTY;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();


        if (reqParcelProperty.ValueEstimate == 0)
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "value estimate required field on property tab have not been Filled.",
                IsPropertyFlow = true
            });

        if (string.IsNullOrEmpty(reqParcelProperty.SourceOfValueEstimate))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "Source of value estimate required field on property tab have not been Filled.",
                IsPropertyFlow = true
            });

        if (property.Status == PropertyStatusEnum.SUBMITTED)
        {
            if (reqParcelProperty.EstimatedPurchasePrice == 0)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Estimate Purchase Price required field on property tab have not been Filled.",
                    IsPropertyFlow = true
                });
            //if (reqParcelProperty.TotalAssessedValue == 0)
            //    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            //    {
            //        ApplicationId = applcation.Id,
            //        PamsPin = property.PamsPin,
            //        SectionId = sectionId,
            //        Message = "Total Assessed value required field on property tab have not been Filled.",
            //        IsPropertyFlow = true
            //    });
            //if (reqParcelProperty.LandValue == 0)
            //    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            //    {
            //        ApplicationId = applcation.Id,
            //        PamsPin = property.PamsPin,
            //        SectionId = sectionId,
            //        Message = "Land value required field on property tab have not been Filled.",
            //        IsPropertyFlow = true
            //    });
            //if (reqParcelProperty.ImprovementValue == 0)
            //    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            //    {
            //        ApplicationId = applcation.Id,
            //        PamsPin = property.PamsPin,
            //        SectionId = sectionId,
            //        Message = "Improvement value required field on property tab have not been Filled.",
            //        IsPropertyFlow = true
            //    });
            if (string.IsNullOrEmpty(reqParcelProperty.NfipPolicyNo))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Nfip Policy Number required field on property tab have not been Filled.",
                    IsPropertyFlow = true
                });

            //if (reqFloodParcel.SquareFootage == 0)
            //    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            //    {
            //        ApplicationId = applcation.Id,
            //        PamsPin = property.PamsPin,
            //        SectionId = sectionId,
            //        Message = "Square Footage value required field on property tab have not been Filled.",
            //        IsPropertyFlow = true
            //    });
            //if (reqFloodParcel.YearOfConstruction == 0)
            //    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            //    {
            //        ApplicationId = applcation.Id,
            //        PamsPin = property.PamsPin,
            //        SectionId = sectionId,
            //        Message = "Year Constructed value required field on property tab have not been Filled.",
            //        IsPropertyFlow = true
            //    });
            if (reqParcelProperty?.StructureType == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "StructureType required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });
            if (reqParcelProperty?.FoundationType == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "FoundationType required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });
            if (reqParcelProperty?.OccupancyClass == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Occupancy Class required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });
            //if (reqParcelProperty?.AnnualTaxes == 0)
            //    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            //    {
            //        ApplicationId = applcation.Id,
            //        PamsPin = property.PamsPin,
            //        SectionId = sectionId,
            //        Message = "Annual Taxes required field on property tab have not been Filled.",
            //        IsPropertyFlow = true
            //    });
            if (reqParcelProperty?.IsRentalProperty == true)
            {
                if (reqParcelProperty.RentPerMonth == 0)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Rent Per Month required field on property tab have not been Filled.",
                        IsPropertyFlow = true
                    });
                }
            }
        }

        if (string.IsNullOrEmpty(reqFloodParcel.Block))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "block required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.Lot))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "Lot required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.StreetNo))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "Street# required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.StreetAddress))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "Street Address required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.OwnersAddress1))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "mailing Address 1 required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.OwnersAddress2))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "Mailing Address 2 required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.OwnersCity))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "City required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.OwnersZipcode))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "ZipCode required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });
        if (string.IsNullOrEmpty(reqFloodParcel.OwnersState))
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                PamsPin = property.PamsPin,
                SectionId = sectionId,
                Message = "State required field on property tab have not been Filled.",
                IsPropertyFlow = false
            });

        return brokenRules;
    }
}

