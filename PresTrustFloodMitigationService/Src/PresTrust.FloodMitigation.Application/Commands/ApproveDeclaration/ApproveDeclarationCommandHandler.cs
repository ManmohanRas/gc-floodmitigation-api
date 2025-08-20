namespace PresTrust.FloodMitigation.Application.Commands;
public class ApproveDeclarationCommandHandler : BaseHandler, IRequestHandler<ApproveDeclarationCommand, ApproveDeclarationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IBrokenRuleRepository repoBrokenRules;
    private readonly IPropertyBrokenRuleRepository repoPropBrokenRules;
    private readonly IEmailManager repoEmailManager;



    public ApproveDeclarationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IBrokenRuleRepository repoBrokenRules,
        IPropertyBrokenRuleRepository repoPropBrokenRules,
        IEmailManager repoEmailManager
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoBrokenRules = repoBrokenRules;
        this.repoPropBrokenRules = repoPropBrokenRules;
        this.repoEmailManager = repoEmailManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApproveDeclarationCommandViewModel> Handle(ApproveDeclarationCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        ApproveDeclarationCommandViewModel result = new ();

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);
        AuthorizationCheck(application);

        // check if any broken rules exists, if yes then return
        var brokenRules = await repoBrokenRules.GetBrokenRulesAsync(application.Id);
        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
            return result;
        }

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.DRAFT;
            application.LastUpdatedBy = userContext.Email;
        }

        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);

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

            // returns broken rules  
            var defaultBrokenRules = ReturnBrokenRulesIfAny(application);
            var defaultPropertyBrokenRules = ReturnPropertyBrokenRulesIfAny(application.Id, appParcels.Select(o => o.PamsPin).ToList());
            // save broken rules
            await repoBrokenRules.SaveBrokenRules(defaultBrokenRules);
            await repoPropBrokenRules.SavePropertyBrokenRules(defaultPropertyBrokenRules);

            //Get Template and Send Email
            //await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_DOI_SUBMITTED_TO_DOI_APPROVED.ToString(), application);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }

    /// <summary>
    /// Ensure that a user has the relevant authorizations to perform an action
    /// </summary>
    private void AuthorizationCheck(FloodApplicationEntity application)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.APPROVE_DECLARATION_OF_INTENT);
    }

    /// <summary>
    /// Return broken rules in case of any business rule failure
    /// </summary>
    /// <param name="request"></param>
    /// <param name="application"></param>
    /// <returns></returns>
    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application)
    {
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        // add default broken rule while initiating application flow
        //brokenRules.Add(new FloodBrokenRuleEntity()
        //{
        //    ApplicationId = application.Id,
        //    SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
        //    Message = "All required fields on Project Area tab have not been filled.",
        //    IsApplicantFlow = true
        //});

        //brokenRules.Add(new FloodBrokenRuleEntity()
        //{
        //    ApplicationId = application.Id,
        //    SectionId = (int)ApplicationSectionEnum.OVERVIEW,
        //    Message = "All required fields on OverView tab have not been filled.",
        //    IsApplicantFlow = true
        //});

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.FINANCE,
            Message = "All required fields on Finance tab have not been filled.",
            IsApplicantFlow = true
        });

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.SIGNATORY,
            Message = "All required fields on Signatory tab have not been filled.",
            IsApplicantFlow = true
        });

        return brokenRules;
    }

    private List<FloodPropertyBrokenRuleEntity> ReturnPropertyBrokenRulesIfAny(int applicationId, List<string> pamsPins)
    {
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();
        
        foreach(var pamsPin in pamsPins)
        {
            brokenRules.Add(new FloodPropertyBrokenRuleEntity() 
            {
                ApplicationId = applicationId,
                SectionId = (int)PropertySectionEnum.PROPERTY,
                PamsPin = pamsPin,
                Message = "All required fields on Property tab have not been filled.",
                IsPropertyFlow = true
            });
        }

        return brokenRules;
    }
}
