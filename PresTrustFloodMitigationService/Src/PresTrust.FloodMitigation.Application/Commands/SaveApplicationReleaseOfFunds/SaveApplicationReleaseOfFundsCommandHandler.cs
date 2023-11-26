using PresTrust.FloodMitigation.Domain.Entities;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationReleaseOfFundsCommandHandler: BaseHandler, IRequestHandler<SaveApplicationReleaseOfFundsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationReleaseOfFundsRepository repoROF;
    private readonly IBrokenRuleRepository repoBrokenRules;

    public SaveApplicationReleaseOfFundsCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationReleaseOfFundsRepository repoROF,
        IBrokenRuleRepository repoBrokenRules
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoROF = repoROF;
        this.repoBrokenRules = repoBrokenRules;
    }

    public async Task<int> Handle(SaveApplicationReleaseOfFundsCommand request, CancellationToken cancellationToken)
    {
        int releaseOfFundsId = 0;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodApplicationReleaseOfFundsEntity
        var reqReleaseOfFunds = mapper.Map<SaveApplicationReleaseOfFundsCommand, FloodApplicationReleaseOfFundsEntity>(request);

        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(application,reqReleaseOfFunds);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.ADMIN_RELEASE_OF_FUNDS);
            // Save current Broken Rules, if any
            await repoBrokenRules.SaveBrokenRules(brokenRules);
           
            var releaseOfFunds = await repoROF.SaveAsync(reqReleaseOfFunds);

            releaseOfFundsId = releaseOfFunds.Id;
            scope.Complete();
        }
        return releaseOfFundsId;

    }
    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application ,FloodApplicationReleaseOfFundsEntity reqReleaseOfFunds)
    {
        int sectionId = (int)ApplicationSectionEnum.ADMIN_RELEASE_OF_FUNDS;
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        // add based on the empty check conditions
         if (application.Status == ApplicationStatusEnum.IN_REVIEW)
         {
                if (string.IsNullOrEmpty(reqReleaseOfFunds?.CAFNumber))
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = sectionId,
                        Message = "CAF Number required field on Project Area Release Of Funds tab have not been filled.",
                        IsApplicantFlow = false
                    });
         }

        if (application.Status == ApplicationStatusEnum.ACTIVE)
        {
            if (reqReleaseOfFunds?.CAFClosed == false)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "CAF Closed required field on Project Area Release Of Funds tab have not been filled.",
                    IsApplicantFlow = false
                });
        }

        return brokenRules;
    }
}
