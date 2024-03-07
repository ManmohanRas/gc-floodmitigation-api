namespace PresTrust.FloodMitigation.Application.Commands;
public class PreservePropertyCommandHandler : BaseHandler, IRequestHandler<PreservePropertyCommand, PreservePropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRules;
    private readonly IPropertyAdminDetailsRepository repoPropertyAdminDetails;
    private readonly IEmailManager repoEmailManager;


    public PreservePropertyCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoProperty,
        IPropertyDocumentRepository repoPropertyDocuments,
        IPropertyBrokenRuleRepository repoPropertyBrokenRules,
        IPropertyAdminDetailsRepository repoPropertyAdminDetails,
        IEmailManager repoEmailManager
    ) : base(repoApplication, repoProperty)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoProperty = repoProperty;
        this.repoPropertyDocuments = repoPropertyDocuments;
        this.repoPropertyBrokenRules = repoPropertyBrokenRules;
        this.repoPropertyAdminDetails = repoPropertyAdminDetails;
        this.repoEmailManager = repoEmailManager;
    }

    /// <summary>
    /// 
    /// </summary>....................... 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PreservePropertyCommandViewModel> Handle(PreservePropertyCommand request, CancellationToken cancellationToken)
    {
        PreservePropertyCommandViewModel result = new ();

        // check if Property exists
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // check if any broken rules exists, if yes then return
        var brokenRules = await repoPropertyBrokenRules.GetPropertyBrokenRulesAsync(property.ApplicationId, property.PamsPin);
        var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, property.PamsPin, property.StatusId, (int)PropertySectionEnum.OTHER_DOCUMENTS);
        if (!hasOtherDocuments)
        {
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = application.Id,
                SectionId = (int)PropertySectionEnum.OTHER_DOCUMENTS,
                PamsPin = property.PamsPin,
                Message = "Required Documents are not uploaded in OtherDocuments Tab",
                IsPropertyFlow = true
            }); 
        }

        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<PropertyBrokenRulesViewModel>>(brokenRules);
            return result;
        }

        //update Property
        if (property != null)
        {
            property.StatusId = (int)PropertyStatusEnum.PRESERVED;
            property.IsLocked = true;
            property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(property);
            FloodParcelStatusLogEntity appParcelStatusLog = new()
            {
                ApplicationId = property.ApplicationId,
                PamsPin = property.PamsPin,
                StatusId = property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appParcelStatusLog);

            var defaultBrokenRules = ReturnBrokenRulesIfAny(application, property);
            // Save current Broken Rules, if any
            await repoPropertyBrokenRules.SavePropertyBrokenRules(defaultBrokenRules);

            await repoProperty.CreateLockedParcel(property.ApplicationId, property.PamsPin, userContext.Email);

            //Get Template and Send Email
            await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_PROPERTY_STATUS_FROM_APPROVED_TO_PRESERVED.ToString(), application, property);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
    private async Task<bool> CheckPropertyOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId, int sectionId)
    {
        var requiredDocumentTypes = new List<int>();

        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE)
        {
            var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

            switch (propertyStatusId)
            {
                case (int)PropertyStatusEnum.APPROVED:
                    requiredDocumentTypes = new List<int>() {
                       (int)PropertyDocumentTypeEnum.SURVEY_LEGAL_DESCRIPTION,
                       (int)PropertyDocumentTypeEnum.TITLE_SEARCH_REPORT,
                    };
                    if (adminDetails.IsDEPInvolved)
                    {
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.SURVEY_REVIEW_LETTER);
                    }
                    if (adminDetails.IsPARRequestedbyFunder)
                    {
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT);
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT_REVIEWIETTER);
                    }
                    else
                    {
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.HOME_OWNERSURVEY);
                    }
                    break;
            }

            var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
            var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();

            return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
        }

        return true;
    }

    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity applcation, FloodApplicationParcelEntity property)
    {
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

        // add default broken rule while initiating application flow\
        if (applcation.ApplicationSubType == ApplicationSubTypeEnum.FASTTRACK)
        {
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applcation.Id,
                SectionId = (int)PropertySectionEnum.ADMIN_DETAILS,
                PamsPin = property.PamsPin,
                Message = "All required fields on ADMIN DETAILS tab have not been filled.",
                IsPropertyFlow = false
            });
        }
        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        {
            ApplicationId = applcation.Id,
            SectionId = (int)PropertySectionEnum.ADMIN_TRACKING,
            PamsPin = property.PamsPin,
            Message = "All required fields on ADMIN TRACKING tab have not been filled.",
            IsPropertyFlow = false
        });
        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        {
            ApplicationId = applcation.Id,
            SectionId = (int)PropertySectionEnum.ADMIN_RELEASE_OF_FUNDS,
            PamsPin = property.PamsPin,
            Message = "All required fields on ADMIN RELEASE OF FUNDS tab have not been filled.",
            IsPropertyFlow = false
        });
        return brokenRules;
    }

}
