using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropReleaseOfFundsCommandHandler : BaseHandler, IRequestHandler<SavePropReleaseOfFundsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IPropReleaseOfFundsRepository repoPropReleaseOfFunds;
    private readonly IParcelPropertyRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationParcelRepository repoAppParcel;

    public SavePropReleaseOfFundsCommandHandler
    (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       IPropReleaseOfFundsRepository repoPropReleaseOfFunds,
       IParcelPropertyRepository repoProperty,
       IPropertyBrokenRuleRepository repoBrokenRules,
       IApplicationParcelRepository repoAppParcel
    ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoPropReleaseOfFunds = repoPropReleaseOfFunds;
        this.repoProperty = repoProperty;
        this.repoBrokenRules = repoBrokenRules;
        this.repoAppParcel = repoAppParcel;     
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SavePropReleaseOfFundsCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // Check Broken Rules
        var reqPropRof = mapper.Map<SavePropReleaseOfFundsCommand, FloodPropReleaseOfFundsEntity>(request);
        // map command object to the FloodTechDetailsEntity
        var brokenRules = ReturnBrokenRulesIfAny(application, property, reqPropRof);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.ADMIN_RELEASE_OF_FUNDS, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);
            reqPropRof.LastUpdatedBy = userContext.Email;
            reqPropRof = await repoPropReleaseOfFunds.SaveAsync(reqPropRof);
            scope.Complete();
        }
        return reqPropRof.Id;
    }
    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodPropReleaseOfFundsEntity reqPropRof)
    {
        int sectionId = (int)PropertySectionEnum.ADMIN_RELEASE_OF_FUNDS;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        if (property.Status == PropertyStatusEnum.APPROVED)
        {
            if (reqPropRof.HardCostPaymentStatus != PaymentStatusEnum.FUNDS_RELEASED)
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = applcation.Id,
                    PamsPin = property.PamsPin,
                    SectionId = sectionId,
                    Message = "Hard Cost Payment Status must be released.",
                    IsPropertyFlow = true
                });
        }

        return brokenRules;
    }


}
