using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class CloseApplicationCommandHandler : BaseHandler, IRequestHandler<CloseApplicationCommand, CloseApplicationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRules;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;

    public CloseApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IPropertyBrokenRuleRepository repoPropertyBrokenRules,
        IPropertyDocumentRepository repoPropertyDocuments
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoPropertyBrokenRules = repoPropertyBrokenRules;
        this.repoPropertyDocuments = repoPropertyDocuments;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CloseApplicationCommandViewModel> Handle(CloseApplicationCommand request, CancellationToken cancellationToken)
    {
        CloseApplicationCommandViewModel result = new();

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.CLOSED;
            application.LastUpdatedBy = userContext.Email;
        }

        bool canCloseApplication = true;

        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
        foreach (var appParcel in appParcels)
        {
            var propBrokenRules = await repoPropertyBrokenRules.GetPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin);
            if (propBrokenRules.Count > 0)
                goto stayBack;
            var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, appParcel.PamsPin, appParcel.StatusId);
            if(!hasOtherDocuments)
                goto stayBack;

            stayBack:
            {
                canCloseApplication = false;
                break;
            }
        }

        if(!canCloseApplication)
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

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }

    private async Task<bool> CheckPropertyOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId)
    {
        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE && propertyStatusId == (int)PropertyStatusEnum.PRESERVED)
        {
            var requiredDocumentTypes = new int[] {
                (int)PropertyDocumentTypeEnum.RECORDED_DEED,
                (int)PropertyDocumentTypeEnum.EXECUTED,
                (int)PropertyDocumentTypeEnum.TITLE_INSURANCE_POLICY
            };
            var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, (int)PropertySectionEnum.OTHER_DOCUMENTS);
            var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToArray();
            return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
        }

        return true;
    }
}
