using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class CloseApplicationCommandHandler : BaseHandler, IRequestHandler<CloseApplicationCommand, CloseApplicationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IParcelPropertyRepository repoParcelProperty;
    private readonly IPropReleaseOfFundsRepository repoPropReleaseOfFunds;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRules;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;
    private readonly IEmailManager repoEmailManager;

    public CloseApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IParcelPropertyRepository repoParcelProperty,
        IPropReleaseOfFundsRepository repoPropReleaseOfFunds,
        IPropertyBrokenRuleRepository repoPropertyBrokenRules,
        IPropertyDocumentRepository repoPropertyDocuments,
        IEmailManager repoEmailManager
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoParcelProperty = repoParcelProperty;
        this.repoPropReleaseOfFunds = repoPropReleaseOfFunds;
        this.repoPropertyBrokenRules = repoPropertyBrokenRules;
        this.repoPropertyDocuments = repoPropertyDocuments;
        this.repoEmailManager   = repoEmailManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CloseApplicationCommandViewModel> Handle(CloseApplicationCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        CloseApplicationCommandViewModel result = new();

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
        var requiredPropertyStatuses = new List<int>()
        {
            (int)PropertyStatusEnum.PRESERVED,
            (int)PropertyStatusEnum.WITHDRAWN,
            (int)PropertyStatusEnum.PROJECT_AREA_EXPIRED,
            (int)PropertyStatusEnum.GRANT_EXPIRED,
            (int)PropertyStatusEnum.TRANSFERRED
        };
        if(appParcels.Where(o => !requiredPropertyStatuses.Contains(o.StatusId)).Count() > 0)
        {
            List<FloodBrokenRuleEntity> brokenRules = new();
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = application.Id,
                SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                Message = "One or more properties have not reached final statuses",
                IsApplicantFlow = true
            });
            result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
            return result;
        }

        appParcels = appParcels.Where(o => o.Status == PropertyStatusEnum.PRESERVED).ToList();
        foreach (var appParcel in appParcels)
        {
            var propBrokenRules = await repoPropertyBrokenRules.GetPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin);
            if (propBrokenRules.Count > 0)
            {
                List<FloodBrokenRuleEntity> brokenRules = new();
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                    Message = "One or more properties are incomplete",
                    IsApplicantFlow = true
                });
                result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
                return result;
            }

            var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, appParcel.PamsPin, appParcel.StatusId);
            if(!hasOtherDocuments)
            {
                List<FloodBrokenRuleEntity> brokenRules = new();
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                    Message = "One or more properties are incomplete",
                    IsApplicantFlow = true
                });
                result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
                return result;
            }

            var parcelProperty = await repoParcelProperty.GetAsync(application.Id, appParcel.PamsPin);
            if(parcelProperty != null && parcelProperty.NeedSoftCost)
            {
                var propertyPayment = await repoPropReleaseOfFunds.GetReleaseOfFundsAsync(application.Id, appParcel.PamsPin);
                if(propertyPayment.SoftCostPaymentStatus != PaymentStatusEnum.FUNDS_RELEASED)
                {
                    List<FloodBrokenRuleEntity> brokenRules = new();
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                        Message = "Softcost payments for one or more properties are not released",
                        IsApplicantFlow = true
                    });
                    result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
                    return result;
                }
            }
        }

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.CLOSED;
            application.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplication.SaveApplicationWorkflowStatusAsync(application);
            FloodApplicationStatusLogEntity appStatusLog = new()
            {
                ApplicationId = application.Id,
                StatusId = application.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = application.LastUpdatedBy
            };
            await repoApplication.SaveStatusLogAsync(appStatusLog);

            await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_ACTIVE_TO_CLOSED.ToString(), application);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }

    private async Task<bool> CheckPropertyOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId)
    {
        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE && propertyStatusId == (int)PropertyStatusEnum.PRESERVED)
        {
            var requiredDocumentTypes = new List<int>() {
                (int)PropertyDocumentTypeEnum.RECORDED_DEED,
                (int)PropertyDocumentTypeEnum.EXECUTED_HUD1,
                (int)PropertyDocumentTypeEnum.TITLE_INSURANCE_POLICY
            };
            var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, (int)PropertySectionEnum.OTHER_DOCUMENTS);
            var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();
            return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
        }

        return true;
    }
}
