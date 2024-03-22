using Microsoft.AspNetCore.Builder;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTechDetailsCommandHandler : BaseHandler, IRequestHandler<SaveTechDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private ITechDetailsRepository repoTechDetails;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IParcelRepository repoParcel;
    private readonly IApplicationParcelRepository repoAppParcel;


    public SaveTechDetailsCommandHandler
   (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       ITechDetailsRepository repoTechDetails,
       IPropertyBrokenRuleRepository repoBrokenRules,
      IParcelRepository repoParcel,
      IApplicationParcelRepository repoAppParcel
    ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoTechDetails = repoTechDetails;
        this.repoBrokenRules = repoBrokenRules;
        this.repoParcel = repoParcel;
        this.repoAppParcel = repoAppParcel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SaveTechDetailsCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        var reqTechDetails = mapper.Map<SaveTechDetailsCommand, FloodTechDetailsEntity>(request);
        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(application, property, reqTechDetails);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.TECH, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);
            reqTechDetails = await repoTechDetails.SaveTechAsync(reqTechDetails);
            reqTechDetails.LastUpdatedBy = userContext.Email;
            scope.Complete();

        }
        return reqTechDetails.Id;
    }
    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application, FloodApplicationParcelEntity property, FloodTechDetailsEntity reqTechDetails)
    {
        int sectionId = (int)PropertySectionEnum.TECH;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        var applicationStatuses = new List<ApplicationStatusEnum>()
        {
            ApplicationStatusEnum.DOI_DRAFT,
            ApplicationStatusEnum.DOI_SUBMITTED,
            ApplicationStatusEnum.DRAFT
        };

        if (!applicationStatuses.Contains(application.Status) && property.Status != PropertyStatusEnum.NONE) 
        { 
            
        

            if (reqTechDetails.BenefitCostRatio == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "BenefitCostRatio required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqTechDetails.FEMACommunityId))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "FEMA Community required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.FirmEffectiveDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Firm Effective Date required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqTechDetails.FirmPanelFinal))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Firm Pannel Effective  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqTechDetails.FloodZoneDesignation))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Flood Zone Designation required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.BaseFloodElevationFinal == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Base Flood Elevation Effective  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.RiverIdFinal == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "River X-Section ID Effective  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.FisEffectiveDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Fis Effective Date required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqTechDetails.FloodProfileFinal))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Flood Profile Effective  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqTechDetails.FloodSource))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Flood Source required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.FirstFloodElevationFinal == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "First Flood Elevation Effective  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.StreambedElevationFinal == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Stream Bed Elevation Effective  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.ElevationBeforeMitigation == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "FFE Before Elevation Mitigation required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.ElevationBeforeMitigationFinal == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "FFE After Elevation Mitigation  required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqTechDetails.FloodType))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Flood Type required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.TenPercent == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Ten Percent required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.TwoPercent == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Two Percent required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.OnePercent == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "One Percent required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });

            if (reqTechDetails.PointOnePercent == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Point One Percent required field on Tech tab have not been filled.",
                    IsPropertyFlow = false
                });
        }
        if (!applicationStatuses.Contains(application.Status) && property.Status == PropertyStatusEnum.SUBMITTED)
        {
            if (reqTechDetails.Claim10Years == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "# of Claims (10 yrs) required field on Tech tab have not been filled.",
                    IsPropertyFlow = true
                });
            if (reqTechDetails.TotalOfClaims == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Total $ Of Claims required field on Tech tab have not been filled.",
                    IsPropertyFlow = true
                });
        }
            return brokenRules;
    }
}
