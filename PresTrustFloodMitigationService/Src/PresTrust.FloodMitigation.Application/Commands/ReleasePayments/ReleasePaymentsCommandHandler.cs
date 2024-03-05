using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleasePaymentsCommandHandler: BaseHandler, IRequestHandler<ReleasePaymentsCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IPropReleaseOfFundsRepository repoROF;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IParcelPropertyRepository repoProperty;


    public ReleasePaymentsCommandHandler(
        IMapper mapper, IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IPropReleaseOfFundsRepository repoROF,
        IPropertyBrokenRuleRepository repoBrokenRules,
        IParcelPropertyRepository repoProperty
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoROF = repoROF;
        this.repoBrokenRules = repoBrokenRules;
        this.repoProperty = repoProperty;
    }

    public async Task<bool> Handle(ReleasePaymentsCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var releaseOfFunds = request.Payments;

        if(releaseOfFunds.Count() > 0)
        {
            using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
            {
                foreach (var payment in releaseOfFunds)
                {
                    var property = await GetIfPropertyExists(payment.ApplicationId, payment.PamsPin);
                    var reqPropRof = mapper.Map<FloodParcelReleaseOfFundsViewModel, FloodPropReleaseOfFundsEntity>(payment);
                    var brokenRules = await ReturnBrokenRulesIfAny(application, property, reqPropRof);
                    await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.ADMIN_RELEASE_OF_FUNDS, payment.PamsPin);
                    await repoBrokenRules.SavePropertyBrokenRules(brokenRules);

                    payment.HardCostPaymentStatus = ((PaymentStatusEnum)payment.HardCostPaymentStatusId).ToString();
                    payment.SoftCostPaymentStatus = ((PaymentStatusEnum)payment.SoftCostPaymentStatusId).ToString();
                    await repoROF.ReleasePayments(mapper.Map<FloodParcelReleaseOfFundsViewModel, FloodPropReleaseOfFundsEntity>(payment));
                }
                scope.Complete();
            }
        }
        return true;
    }

    private async Task<List<FloodPropertyBrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property, FloodPropReleaseOfFundsEntity reqPropRof)
    {
        int sectionId = (int)PropertySectionEnum.ADMIN_RELEASE_OF_FUNDS;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        var reqPropDetails = await repoProperty.GetAsync(applcation.Id, property.PamsPin);

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

        if (property.Status == PropertyStatusEnum.PRESERVED)
        {
            if (reqPropDetails.NeedSoftCost == true)
            {
                if (reqPropRof.SoftCostPaymentStatus == PaymentStatusEnum.FUNDS_NOT_RELEASED)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Soft cost Payment must be released in Release of funds tab.",
                        IsPropertyFlow = false
                    });
                }
            }
        }

        return brokenRules;
    }
}
