namespace PresTrust.FloodMitigation.Application.Commands;
public class ApproveDeclarationCommandHandler : BaseHandler, IRequestHandler<ApproveDeclarationCommand, ApproveDeclarationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IBrokenRuleRepository repoBrokenRules;

    public ApproveDeclarationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IBrokenRuleRepository repoBrokenRules
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;  
        this.repoBrokenRules = repoBrokenRules; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApproveDeclarationCommandViewModel> Handle(ApproveDeclarationCommand request, CancellationToken cancellationToken)
    {
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
            //change properties statuses to DOI_SUBMITTED in future

            // returns broken rules  
            var defaultBrokenRules = ReturnBrokenRulesIfAny(application);
            // save broken rules
            await repoBrokenRules.SaveBrokenRules(defaultBrokenRules);

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
        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.ROLES,
            Message = "All required fields on DOI tab have not been filled.",
            IsApplicantFlow = true
        });
        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
            Message = "All required fields on DOI tab have not been filled.",
            IsApplicantFlow = true
        });

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.OVERVIEW,
            Message = "All required fields on DOI tab have not been filled.",
            IsApplicantFlow = true
        });

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.FINANCE,
            Message = "All required fields on DOI tab have not been filled.",
            IsApplicantFlow = true
        });

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.SIGNATORY,
            Message = "All required fields on DOI tab have not been filled.",
            IsApplicantFlow = true
        });

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.OTHER_DOCUMENTS,
            Message = "All required fields on DOI tab have not been filled.",
            IsApplicantFlow = true
        });

        return brokenRules;
    }

}
