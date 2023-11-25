using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Collections.Specialized.BitVector32;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftCostCommandHandler : BaseHandler, IRequestHandler<SaveSoftCostCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ISoftCostRepository repoSoftCost;
    private readonly IParcelFinanceRepository repoParcelFinance;
    private IPropertyAdminDetailsRepository repoPropertyDetails;
    private readonly IPropertyBrokenRuleRepository repoBrokenRules;
    private readonly IParcelRepository repoParcel;
    private readonly IApplicationParcelRepository repoAppParcel;
    private readonly IPropertyDocumentRepository repoPropertyDocument;

    public SaveSoftCostCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            ISoftCostRepository repoSoftCost,
            IParcelFinanceRepository repoParcelFinance,
            IApplicationParcelRepository repoAppParcel,
            IPropertyDocumentRepository repoPropertyDocument,
            IPropertyBrokenRuleRepository repoBrokenRules
        ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSoftCost = repoSoftCost;
        this.repoParcelFinance = repoParcelFinance;
        this.repoAppParcel = repoAppParcel;
        this.repoPropertyDocument = repoPropertyDocument;
        this.repoBrokenRules = repoBrokenRules;
    }

    public async Task<Unit> Handle(SaveSoftCostCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        var reqPropDetails = mapper.Map<SaveSoftCostCommand, FloodParcelPropertyEntity>(request);
        var reqRof = mapper.Map<SaveSoftCostCommand, FloodPropReleaseOfFundsEntity>(request);
        var reqPropAdminDetails = mapper.Map<SaveSoftCostCommand, FloodPropertyAdminDetailsEntity>(request);
        // Check Broken Rules
        var brokenRules = await ReturnBrokenRulesIfAny(application, property,reqPropDetails, reqRof, reqPropAdminDetails);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.SOFT_COSTS, property.PamsPin);
            // Save current Broken Rules, if any
            await repoBrokenRules.SavePropertyBrokenRules(brokenRules);

            decimal softCostFMPAmt = 0;
            foreach (var softCost in request.SoftCostLineItems)
            {
                var entity = mapper.Map<SaveSoftCostModel, FloodParcelSoftCostEntity>(softCost);
                entity.ApplicationId = request.ApplicationId;
                entity.PamsPin = request.PamsPin;
                await this.repoSoftCost.SaveAsync(entity);
                softCostFMPAmt += softCost.PaymentAmount;
            }

            var parcelFinance = await repoParcelFinance.GetParceFinanceAsync(request.ApplicationId, request.PamsPin);
            if (parcelFinance != null)
            {
                parcelFinance.ApplicationId = request.ApplicationId;
                parcelFinance.PamsPin = request.PamsPin;
                parcelFinance.SoftCostFMPAmt = softCostFMPAmt;
                await repoParcelFinance.SaveAsync(parcelFinance);
            }

            scope.Complete();
        }
        return Unit.Value;
    }
    private async Task<List<FloodPropertyBrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property,FloodParcelPropertyEntity reqPropDetails, FloodPropReleaseOfFundsEntity reqRof, FloodPropertyAdminDetailsEntity reqPropAdminDetails)
    {
        int sectionId = (int)PropertySectionEnum.SOFT_COSTS;
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        var documents = await repoPropertyDocument.GetPropertyDocumentsAsync(applcation.Id, property.PamsPin, sectionId);

        FloodPropertyDocumentEntity? docFmcSoftCostReimbApprovalResolution = default;
        FloodPropertyDocumentEntity? docBccSoftCostReimbApprovalResolution = default;

        docFmcSoftCostReimbApprovalResolution = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.FMC_SOFTCOST).FirstOrDefault();
        docBccSoftCostReimbApprovalResolution = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.FMC_FINAL_APPROVAL).FirstOrDefault();

        if (property.Status == PropertyStatusEnum.PRESERVED)
        {
            if (reqPropDetails.NeedSoftCost == true)
            {
                if (reqRof.SoftCostPaymentStatus == PaymentStatusEnum.FUNDS_NOT_RELEASED )
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Patyments must be released in Release of funds tab.",
                        IsPropertyFlow = false
                    });
                }
            }
        }
        if (applcation.ApplicationSubType == ApplicationSubTypeEnum.FASTTRACK)
        {
            if (reqPropAdminDetails.FmcSoftCostReimbApprovalDate == null && reqPropAdminDetails.FmcSoftCostReimbApprovalNumber == null && reqPropAdminDetails.BccSoftCostReimbApprovalDate == null && reqPropAdminDetails.BccSoftCostReimbApprovalNumber == null)
            {
                if (reqRof.SoftCostPaymentStatus == PaymentStatusEnum.FUNDS_NOT_RELEASED)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = applcation.Id,
                        PamsPin = property.PamsPin,
                        SectionId = sectionId,
                        Message = "Patyments must be released in Release of funds tab.",
                        IsPropertyFlow = false
                    });
                }
            }
        }

        return brokenRules; 
    }
}
