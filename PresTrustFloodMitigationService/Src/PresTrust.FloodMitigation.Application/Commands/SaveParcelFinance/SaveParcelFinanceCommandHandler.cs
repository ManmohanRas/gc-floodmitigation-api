using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelFinanceCommandHandler :BaseHandler, IRequestHandler<SaveParcelFinanceCommand, int>
{
    private IMapper mapper;
    private IParcelFinanceRepository repoParcelFinance;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelRepository repoParcel;
    private readonly IApplicationParcelRepository repoAppParcel;

    public SaveParcelFinanceCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IParcelFinanceRepository repoParcelFinance,
        IPropertyBrokenRuleRepository repoBrokenRules,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IParcelRepository repoParcel,
        IApplicationParcelRepository repoAppParcel
        ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoParcelFinance = repoParcelFinance;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoBrokenRules = repoBrokenRules;
        this.repoApplication = repoApplication;
        this.repoParcel = repoParcel;
        this.repoAppParcel = repoAppParcel; 
    }
    public async Task<int> Handle(SaveParcelFinanceCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        var reqParcelFinance = mapper.Map<SaveParcelFinanceCommand, FloodParcelFinanceEntity>(request);
        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(application, property, reqParcelFinance);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.FINANCE, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);
            reqParcelFinance = await repoParcelFinance.SaveAsync(reqParcelFinance);
            scope.Complete();
        }
        return reqParcelFinance.Id;
    }

    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodParcelFinanceEntity reqParcelFinance)
    {
        int sectionId = (int)PropertySectionEnum.FINANCE;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

        if (applcation.Status == ApplicationStatusEnum.ACTIVE)
        {
            if (property.Status == PropertyStatusEnum.PENDING)
            { 
                if (reqParcelFinance.AppraisedValue <= 0)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Appraised Value required field on Finance tab has not been filled.",
                        IsPropertyFlow = true
                    });
                }

                if (reqParcelFinance.AMV <= 0)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Amv Value required field on Finance tab has not been filled.",
                        IsPropertyFlow = true
                    });
                }

            }
        }
        return brokenRules;
    }
}
