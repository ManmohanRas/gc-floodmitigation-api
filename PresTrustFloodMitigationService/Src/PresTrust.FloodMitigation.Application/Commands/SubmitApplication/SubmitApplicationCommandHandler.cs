namespace PresTrust.FloodMitigation.Application.Commands;
public class SubmitApplicationCommandHandler : BaseHandler, IRequestHandler<SubmitApplicationCommand, SubmitApplicationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationDocumentRepository repoApplicationDocument;
    private readonly IBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IEmailManager repoEmailManager;


    public SubmitApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationDocumentRepository repoApplicationDocument,
        IBrokenRuleRepository repoBrokenRules,
        IApplicationParcelRepository repoApplicationParcel,
        IEmailManager repoEmailManager
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationDocument = repoApplicationDocument;
        this.repoBrokenRules = repoBrokenRules;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoEmailManager = repoEmailManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SubmitApplicationCommandViewModel> Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
    {
        SubmitApplicationCommandViewModel result = new();

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);
        AuthorizationCheck(application);

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.SUBMITTED;
            application.LastUpdatedBy = userContext.Email;
        }
      
        // check if any broken rules exists, if yes then return
        var brokenRules = (await repoBrokenRules.GetBrokenRulesAsync(application.Id))?.ToList();

        bool hasNonSubmittedParcels = false;
        var parcels = await repoApplicationParcel.GetApplicationPropertiesAsync(request.ApplicationId);
        hasNonSubmittedParcels = parcels.Count(o => o.Status != PropertyStatusEnum.SUBMITTED) > 0;

       if (hasNonSubmittedParcels)
        {
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = application.Id,
                SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                Message = "All the Properties must be submitted"
            });
        }

        var otherdocRules = await CheckApplicationOtherDocs(application.Id, application.ApplicationTypeId, (int)ApplicationSectionEnum.OTHER_DOCUMENTS);
        if (otherdocRules.Count > 0)
        {
            brokenRules.AddRange(otherdocRules);
        }

        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
            return result;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // returns broken rules  
            var defaultBrokenRules = ReturnBrokenRulesIfAny(application);
            // save broken rules
            await repoBrokenRules.SaveBrokenRules(defaultBrokenRules);
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

            //Get Template and Send Email
            await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_DOI_APPROVED_TO_SUBMITTED.ToString(), application);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }

    private void AuthorizationCheck(FloodApplicationEntity application)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.SUBMIT_APPLICATION);
    }

    private async Task<List<FloodBrokenRuleEntity>> CheckApplicationOtherDocs(int applicationId, int applicationTypeId, int sectionId)
    {
        var documents = await repoApplicationDocument.GetApplicationDocumentsAsync(applicationId, sectionId);

        List<FloodBrokenRuleEntity> otherdocRules = new List<FloodBrokenRuleEntity>();
        if (applicationTypeId == (int)ApplicationTypeEnum.CORE)
        {
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.APPLICATION_CHECKLIST).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "APPLICATION_CHECKLIST documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.PUBLIC_HEARING_CERTIFICATE).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "PUBLIC_HEARING_CERTIFICATE documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.MINUTES_FROM_PUBLIC_HEARING).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "MINUTES_FROM_PUBLIC_HEARING documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.MUNICIPAL_RESOLUTION_OF_SUPPORT).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "MUNICIPAL_RESOLUTION_OF_SUPPORT documents is not uploaded in OtherDocuments Tab"
                });
            }
        }
        else if (applicationTypeId == (int)ApplicationTypeEnum.MATCH)
        {
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.APPLICATION_CHECKLIST).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "APPLICATION_CHECKLIST documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.PUBLIC_HEARING_CERTIFICATE).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "PUBLIC_HEARING_CERTIFICATE documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.MINUTES_FROM_PUBLIC_HEARING).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "MINUTES_FROM_PUBLIC_HEARING documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.MUNICIPAL_RESOLUTION_OF_SUPPORT).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "MUNICIPAL_RESOLUTION_OF_SUPPORT documents is not uploaded in OtherDocuments Tab"
                });
            }
            if (documents.Where(o => o.DocumentTypeId == (int)ApplicationDocumentTypeEnum.NON_COUNTY_AGENCY_RESOLUTION).Count() == 0)
            {
                otherdocRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = applicationId,
                    SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                    Message = "NON_COUNTY_AGENCY_RESOLUTION documents is not uploaded in OtherDocuments Tab"
                });
            }
        }

        return otherdocRules;
    }



    /// <summary>
    /// Return broken rules in case of any business rule failure
    /// </summary>
    /// <param name="request"></param>
    /// <param name="application"></param>
    /// <returns></returns>
    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application)
    {
        List<FloodBrokenRuleEntity> statusChangeRules = new List<FloodBrokenRuleEntity>();

        // add default broken rule while initiating application flow
        statusChangeRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.ADMIN_DOCUMENT_CHECKLIST,
            Message = "All required fields on ADMIN_DOCUMENT_CHECKLIST tab have not been filled.",
            IsApplicantFlow = true
        });

        statusChangeRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = application.Id,
                SectionId = (int)ApplicationSectionEnum.ADMIN_DETAILS,
                Message = "All required fields on Admin Details tab have not been filled.",
                IsApplicantFlow = false
            });
            
            if (application.ApplicationType == ApplicationTypeEnum.MATCH || application.ApplicationSubType == ApplicationSubTypeEnum.FASTTRACK)
            {
                statusChangeRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = (int)ApplicationSectionEnum.OVERVIEW,
                    Message = "All required fields on OVERVIEW tab have not been filled.",
                    IsApplicantFlow = false
                });

            }
            //statusChangeRules.Add(new FloodBrokenRuleEntity()
            //{
            //    ApplicationId = application.Id,
            //    SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
            //    Message = "All required fields on PROJECT_AREA tab have not been filled.",
            //    IsApplicantFlow = false
            //});

        return statusChangeRules;
    }
}
