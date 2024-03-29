using MediatR;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationAdminDetailsCommandHandler : BaseHandler, IRequestHandler<SaveApplicationAdminDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationDetailsRepository repoAppDetails;
    private readonly IBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationDocumentRepository repoDocuments;

    public SaveApplicationAdminDetailsCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationDetailsRepository repoAppDetails,
        IBrokenRuleRepository repoBrokenRules,
        IApplicationDocumentRepository repoDocuments
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoAppDetails = repoAppDetails;
        this.repoBrokenRules = repoBrokenRules;
        this.repoDocuments = repoDocuments;
    }

    public async Task<int> Handle(SaveApplicationAdminDetailsCommand request, CancellationToken cancellationToken)
    {
        int AppDetailsId = 0;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodApplicationDetailsEntity
        var reqAppDetails = mapper.Map<SaveApplicationAdminDetailsCommand, FloodApplicationAdminDetailsEntity>(request);

        // Check Broken Rules
        var brokenRules = await ReturnBrokenRulesIfAny(application, reqAppDetails);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoBrokenRules.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.ADMIN_DETAILS);
            await repoBrokenRules.SaveBrokenRules(brokenRules);

            var AppDetails = await repoAppDetails.SaveAsync(reqAppDetails);

            application.ExpirationDate = AppDetails.SecondFundingExpirationDate ?? AppDetails.FirstFundingExpirationDate ?? AppDetails.FundingExpirationDate ?? application.ExpirationDate;
            application = await repoApplication.SaveAsync(application);

            AppDetailsId = AppDetails.Id;
            scope.Complete();
        }
        return AppDetailsId;

    }

    private async Task<List<FloodBrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity application, FloodApplicationAdminDetailsEntity AppDetails)
    {
        int sectionId = (int)ApplicationSectionEnum.ADMIN_DETAILS;
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        var documents = await repoDocuments.GetApplicationDocumentsAsync(AppDetails.ApplicationId, sectionId);

        FloodApplicationDocumentEntity docFmcPreliminaryApprovalResolution = default;
        FloodApplicationDocumentEntity docBccPreliminaryApprovalResolution = default;
        FloodApplicationDocumentEntity docProjectAreaApplicationMap = default;
        FloodApplicationDocumentEntity docCoreReviewReport = default;
        FloodApplicationDocumentEntity docProjectArea = default;
        FloodApplicationDocumentEntity docCoreApplicationReport = default;
        FloodApplicationDocumentEntity docsNotificationOfapproval = default;
        FloodApplicationDocumentEntity docsCafCloseOutSummary = default;


        if (documents != null && documents.Count() > 0)
        {
            docFmcPreliminaryApprovalResolution = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.FMC_PRELIMINARY_APPROVAL_RESOLUTION).FirstOrDefault();
            docBccPreliminaryApprovalResolution = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.BCC_PRELIMINARY_APPROVAL_RESOLUTION).FirstOrDefault();
            docProjectAreaApplicationMap = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.PROJECT_AREA_APPLICATION_MAP).FirstOrDefault();
            docCoreReviewReport = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.CORE_REVIEW_REPORT).FirstOrDefault();
            docProjectArea = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.PROJECT_AREA_FUNDS_EXPIRATION_REQUEST).FirstOrDefault();
            docCoreApplicationReport = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.CORE_APPLICATION_REPORT).FirstOrDefault();
            docsNotificationOfapproval = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.NOTIFICATION_OF_APPROVAL_AND_PROCEDURES_LETTER).FirstOrDefault();
            docsCafCloseOutSummary = documents.Where(d => d.DocumentTypeId == (int)ApplicationDocumentTypeEnum.CAF_CLOSE_OUT_SUMMARY).FirstOrDefault();

        }

       if (AppDetails.MunicipalResolutionDate == null)
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = AppDetails.ApplicationId,
                SectionId = sectionId,
               Message = "Municipal Resolution Date required field on AdminDetails tab have not been filled.",
              IsApplicantFlow = false
           });

        if (string.IsNullOrEmpty(AppDetails.MunicipalResolutionNumber))
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = AppDetails.ApplicationId,
                SectionId = sectionId,
                Message = "Municipal Resolution Number required field on AdminDetails tab have not been filled.",
                IsApplicantFlow = false
            });
        if (application.ApplicationType != ApplicationTypeEnum.MATCH && application.ApplicationSubType != ApplicationSubTypeEnum.FASTTRACK)
        {

            if (string.IsNullOrEmpty(AppDetails.ProjectDescription))
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Project Description required field on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });
            if (docProjectAreaApplicationMap == null)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Project Area Application Map required document on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });
            if (docCoreApplicationReport == null)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Core Application Report required document on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });
            if (docCoreReviewReport == null)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Core Review Report required document on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });
        }


        // Active State Broken Rules 
        if (application.Status == ApplicationStatusEnum.ACTIVE)
        {
            if (AppDetails.FundingExpirationDate == null)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Funding Expiration Date required field on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });

            if (AppDetails.FirstFundingExpirationDate != null || AppDetails.SecondFundingExpirationDate != null)
            {
                if (docProjectArea == null)
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = AppDetails.ApplicationId,
                        SectionId = sectionId,
                        Message = "Project Area Funds Expiration Request required document on AdminDetails tab have not been filled.",
                        IsApplicantFlow = false
                    });
            }

            if (docsCafCloseOutSummary == null)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Caf Close out summary required document on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });
            if (docsNotificationOfapproval == null)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = AppDetails.ApplicationId,
                    SectionId = sectionId,
                    Message = "Notification of Approval and Procedures Letter required document on AdminDetails tab have not been filled.",
                    IsApplicantFlow = false
                });
        }

        // All application type Match Broken Rules 
        if (application.ApplicationType == ApplicationTypeEnum.MATCH && application.ApplicationSubType != ApplicationSubTypeEnum.FASTTRACK)
        {
            if (application.Status == ApplicationStatusEnum.SUBMITTED)
            {
                if (string.IsNullOrEmpty(AppDetails.ProjectDescription))
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = AppDetails.ApplicationId,
                        SectionId = sectionId,
                        Message = "Project Description required field on AdminDetails tab have not been filled.",
                        IsApplicantFlow = false
                    });
                if (docProjectAreaApplicationMap == null)
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = AppDetails.ApplicationId,
                        SectionId = sectionId,
                        Message = "Project Area Application Map required document on AdminDetails tab have not been filled.",
                        IsApplicantFlow = false
                    });
                if (docCoreReviewReport == null)
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = AppDetails.ApplicationId,
                        SectionId = sectionId,
                        Message = "Core Review Report required document on AdminDetails tab have not been filled.",
                        IsApplicantFlow = false
                    });
                if (docCoreApplicationReport == null)
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = AppDetails.ApplicationId,
                        SectionId = sectionId,
                        Message = "Core Application Report required document on AdminDetails tab have not been filled.",
                        IsApplicantFlow = false
                    });
            }
        }

             if (application.Status == ApplicationStatusEnum.IN_REVIEW)
             {
                if (application.ApplicationSubType != ApplicationSubTypeEnum.FASTTRACK)
                {
                    if (AppDetails.FMCPreliminaryApprovalDate == null)
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "FMC Preliminary Approval Date required field on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                    if (string.IsNullOrEmpty(AppDetails.FMCPreliminaryNumber))
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "FMC Preliminary Number required field on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                    if (AppDetails.BCCPreliminaryApprovalDate == null)
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "BCC Preliminary Approval Date required field on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                    if (string.IsNullOrEmpty(AppDetails.BCCPreliminaryNumber))
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "BCC Preliminary Number required field on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                    if (docFmcPreliminaryApprovalResolution == null)
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "FMC Preliminary Approval Resolution required document on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                    if (docBccPreliminaryApprovalResolution == null)
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "BCC Preliminary Approval Resolution required document on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                    if (docsNotificationOfapproval == null)
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = AppDetails.ApplicationId,
                            SectionId = sectionId,
                            Message = "Notification Of approval required document on AdminDetails tab have not been filled.",
                            IsApplicantFlow = false
                        });
                }
             }
        return brokenRules;
    }
    
}
        
    


