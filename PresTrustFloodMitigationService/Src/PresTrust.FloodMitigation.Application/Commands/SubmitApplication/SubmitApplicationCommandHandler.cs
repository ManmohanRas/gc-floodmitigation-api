using PresTrust.FloodMitigation.Application.CommonViewModels;

namespace PresTrust.FloodMitigation.Application.Commands;
public class SubmitApplicationCommandHandler : BaseHandler, IRequestHandler<SubmitApplicationCommand, SubmitApplicationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationDocumentRepository repoApplicationDocument;
    private readonly IBrokenRuleRepository repoBrokenRules;

    public SubmitApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationDocumentRepository repoApplicationDocument,
        IBrokenRuleRepository repoBrokenRules

    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication; 
        this.repoApplicationDocument = repoApplicationDocument;
        this.repoBrokenRules = repoBrokenRules;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SubmitApplicationCommandViewModel> Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
    {
        SubmitApplicationCommandViewModel result = new ();

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

        var hasOtherDocuments = await CheckApplicationOtherDocs(application.Id, application.ApplicationTypeId, (int)ApplicationSectionEnum.OTHER_DOCUMENTS);
        if (!hasOtherDocuments)
        {
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = application.Id,
                SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
                Message = "Required Documents are not uploaded in OtherDocuments Tab"
            });
        }

        if (brokenRules != null && brokenRules.Any())
        {
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
            //change properties statuses to submitted in future

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

    private async Task<bool> CheckApplicationOtherDocs(int applicationId, int applicationTypeId, int sectionId)
    {
        bool hasOtherDocuments = false;

        var documents = await repoApplicationDocument.GetApplicationDocumentsAsync(applicationId, sectionId);

        FloodApplicationDocumentEntity applicationChecklist = default;
        FloodApplicationDocumentEntity publicHearing = default;
        FloodApplicationDocumentEntity municipalResolution = default;
        FloodApplicationDocumentEntity minutesFromPublicHearing = default;
        FloodApplicationDocumentEntity noncountyAgancyResolution = default;

        if (documents != null && documents.Count() > 0)
        {
            applicationChecklist = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.APPLICATION_CHECKLIST).FirstOrDefault();
            publicHearing = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.PUBLIC_HEARING_CERTIFICATE).FirstOrDefault();
            municipalResolution = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.MUNICIPAL_RESOLUTION_OF_SUPPORT).FirstOrDefault();
            minutesFromPublicHearing = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.MINUTES_FROM_PUBLIC_HEARING).FirstOrDefault();
            noncountyAgancyResolution = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.NON_COUNTY_AGENCY_RESOLUTION).FirstOrDefault();
        }

        if (applicationTypeId == (int)ApplicationTypeEnum.CORE)
        {
            if (applicationChecklist == null || publicHearing == null || municipalResolution == null || minutesFromPublicHearing == null)
            {
                hasOtherDocuments = false;
            }
            else
            {
                hasOtherDocuments = true;
            }
        }
        else
        {
            hasOtherDocuments = true;
        }

        if(applicationTypeId ==  (int)ApplicationTypeEnum.MATCH)
        {
            if (noncountyAgancyResolution == null)
            {
                hasOtherDocuments = false;
            }
            else
            {
                hasOtherDocuments = true;
            }

        }
        else
        {
            hasOtherDocuments = true;
        }

        return hasOtherDocuments;
    }
}
