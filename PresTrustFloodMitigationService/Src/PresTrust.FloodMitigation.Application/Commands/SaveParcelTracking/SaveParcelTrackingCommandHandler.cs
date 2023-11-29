using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelTrackingCommandHandler : BaseHandler,IRequestHandler<SaveParcelTrackingCommand, int>
{
    private IMapper mapper;
    private IParcelTrackingRepository repoParcelTracking;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelPropertyRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationParcelRepository repoAppParcel;
    private readonly IPropertyDocumentRepository repoPropertyDocument;
    private readonly SystemParameterConfiguration systemParamOptions;


    public SaveParcelTrackingCommandHandler(
        IMapper mapper,
        IParcelTrackingRepository repoParcelTracking,
        IParcelPropertyRepository repoProperty,
        IApplicationRepository repoApplication,
        IPropertyBrokenRuleRepository repoBrokenRules,
        IApplicationParcelRepository repoAppParcel,
        IPropertyDocumentRepository repoPropertyDocument,
    IOptions<SystemParameterConfiguration> systemParamOptions
        ) : base (repoApplication :repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.repoParcelTracking = repoParcelTracking;
        this.repoProperty = repoProperty;
        this.repoBrokenRules = repoBrokenRules;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoPropertyDocument = repoPropertyDocument;
    }

    public async Task<int> Handle(SaveParcelTrackingCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // Check Broken Rules
        var reqParcelTracking = mapper.Map<SaveParcelTrackingCommand, FloodParcelTrackingEntity>(request);

        var brokenRules = await ReturnBrokenRulesIfAny(application, property, reqParcelTracking);
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.ADMIN_TRACKING, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);
            reqParcelTracking = await repoParcelTracking.SaveAsync(reqParcelTracking);
            scope.Complete();
        }
        return reqParcelTracking.Id;
    }

    private async Task<List<FloodPropertyBrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodParcelTrackingEntity reqParcelTracking)
    {
        int sectionId = (int)PropertySectionEnum.ADMIN_TRACKING;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

        var documents = await repoPropertyDocument.GetPropertyDocumentsAsync(applcation.Id, property.PamsPin, sectionId);

        FloodPropertyDocumentEntity? docPostAcquisionPicture = default;

        if (documents != null && documents.Count() > 0)
        {
            docPostAcquisionPicture = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.POST_ACQUISITION).FirstOrDefault();
        }

        if (property.Status == PropertyStatusEnum.APPROVED) 
        {
            if (reqParcelTracking.ClosingDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Closing date required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });
        }

        if (property.Status == PropertyStatusEnum.PRESERVED)
        {
            if (string.IsNullOrEmpty(reqParcelTracking.DeedBook))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Deed Book required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });

            if (string.IsNullOrEmpty(reqParcelTracking.DeedPage))
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Deed Page required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });

            if (reqParcelTracking.DeedDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Deed date required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });
            if (reqParcelTracking.DemolitionDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Demolition Date required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });
            if (reqParcelTracking.SiteVisitConfirmDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "SiteVisit Confirm Date required field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });

            if (docPostAcquisionPicture == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = " Post Acquision Picture required document field on property tab have not been Filled.",
                    IsPropertyFlow = false
                });

            if (reqParcelTracking?.PublicPark == null || reqParcelTracking?.RainGarden == null || reqParcelTracking?.CommunityGarden == null || reqParcelTracking?.ActiveRecreation == null || reqParcelTracking?.NaturalHabitat == null)
            {
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = " Atleast one of the post preservation use must be selected in Tracking tab.",
                    IsPropertyFlow = false
                });
            }
        }

        return brokenRules;
    }

}
