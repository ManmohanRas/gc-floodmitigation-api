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
    private readonly SystemParameterConfiguration systemParamOptions;


    public SaveParcelTrackingCommandHandler(
        IMapper mapper,
        IParcelTrackingRepository repoParcelTracking,
        IParcelPropertyRepository repoProperty,
        IApplicationRepository repoApplication,
        IPropertyBrokenRuleRepository repoBrokenRules,
        IApplicationParcelRepository repoAppParcel,
        IOptions<SystemParameterConfiguration> systemParamOptions
        ) : base (repoApplication :repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.repoParcelTracking = repoParcelTracking;
        this.repoProperty = repoProperty;
        this.repoBrokenRules = repoBrokenRules;
        this.systemParamOptions = systemParamOptions.Value;
    }

    public async Task<int> Handle(SaveParcelTrackingCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // Check Broken Rules
        var reqParcelTracking = mapper.Map<SaveParcelTrackingCommand, FloodParcelTrackingEntity>(request);

        var brokenRules = ReturnBrokenRulesIfAny(application, property, reqParcelTracking);
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

    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodParcelTrackingEntity reqParcelTracking)
    {
        int sectionId = (int)PropertySectionEnum.ADMIN_TRACKING;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        if (property.Status == PropertyStatusEnum.APPROVED) 
        {
            if (reqParcelTracking.ClosingDate == null)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Closing date required field on property tab have not been Filled.",
                    IsPropertyFlow = true
                });
        }

        return brokenRules;
    }

}
