using Newtonsoft.Json.Linq;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class ActivateApplicationCommandHandler : BaseHandler, IRequestHandler<ActivateApplicationCommand, ActivateApplicationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IPropertyBrokenRuleRepository repoPropBrokenRules;
    private readonly IEmailManager repoEmailManager;


    public ActivateApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IBrokenRuleRepository repoBrokenRules,
        IApplicationParcelRepository repoApplicationParcel,
        IPropertyBrokenRuleRepository repoPropBrokenRules,
        IEmailManager repoEmailManager
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;  
        this.repoBrokenRules = repoBrokenRules;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoPropBrokenRules = repoPropBrokenRules;
        this.repoEmailManager = repoEmailManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ActivateApplicationCommandViewModel> Handle(ActivateApplicationCommand request, CancellationToken cancellationToken)
    {
        ActivateApplicationCommandViewModel result = new ();

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        // check if any broken rules exists, if yes then return
        var brokenRules = (await repoBrokenRules.GetBrokenRulesAsync(application.Id))?.ToList();
        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<ApplicationBrokenRuleViewModel>>(brokenRules);
            return result;
        }

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.ACTIVE;
            application.LastUpdatedBy = userContext.Email;
        }

        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);

        //update application parcels
        foreach (var appParcel in appParcels)
        {
            var propBrokenRules = (await repoPropBrokenRules.GetPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin))?.ToList();
            if (propBrokenRules != null && propBrokenRules.Any())
            {
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

            appParcel.StatusId = (int)PropertyStatusEnum.PENDING;
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

            foreach (var appParcel in appParcels)
            {
                await repoApplicationParcel.SaveApplicationParcelWorkflowStatusAsync(appParcel);
                FloodParcelStatusLogEntity appParcelStatusLog = new()
                {
                    ApplicationId = appParcel.ApplicationId,
                    PamsPin = appParcel.PamsPin,
                    StatusId = appParcel.StatusId,
                    StatusDate = DateTime.Now,
                    Notes = string.Empty,
                    LastUpdatedBy = appParcel.LastUpdatedBy
                };
                await repoApplicationParcel.SaveStatusLogAsync(appParcelStatusLog);
            }

            // returns broken rules  
            var defaultBrokenRules = ReturnBrokenRulesIfAny(application);
            var defaultPropertyBrokenRules = ReturnPropertyBrokenRulesIfAny(application.Id, appParcels.Select(o => o.PamsPin).ToList());
            // save broken rules
            await repoBrokenRules.SaveBrokenRules(defaultBrokenRules);
            await repoPropBrokenRules.SavePropertyBrokenRules(defaultPropertyBrokenRules);

            await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_IN_REVIEW_TO_ACTIVE.ToString(), application);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
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
            SectionId = (int)ApplicationSectionEnum.ADMIN_DETAILS,
            Message = "All required fields on ADMIN DETAILS tab have not been filled.",
            IsApplicantFlow = false
        });

        brokenRules.Add(new FloodBrokenRuleEntity()
        {
            ApplicationId = application.Id,
            SectionId = (int)ApplicationSectionEnum.ADMIN_RELEASE_OF_FUNDS,
            Message = "All required fields on ADMIN RELEASE OF FUNDS have not been filled.",
            IsApplicantFlow = false
        });

        return brokenRules;
    }

    /// <summary>
    /// Return broken rules in case of any business rule failure
    /// </summary>
    /// <param name="request"></param>
    /// <param name="application"></param>
    /// <returns></returns>
    private List<FloodPropertyBrokenRuleEntity> ReturnPropertyBrokenRulesIfAny(int applicationId, List<string> pamsPins)
    {
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

        foreach (var pamsPin in pamsPins)
        {
            // add default broken rule while initiating application flow
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applicationId,
                SectionId = (int)PropertySectionEnum.ADMIN_DETAILS,
                PamsPin = pamsPin,
                Message = "All required fields on ADMIN_DETAILS tab have not been filled.",
                IsPropertyFlow = false
            });
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = applicationId,
                SectionId = (int)PropertySectionEnum.FINANCE,
                PamsPin = pamsPin,
                Message = "All required fields on FINANCE tab have not been filled.",
                IsPropertyFlow = false
            });
        }

        return brokenRules;
    }
}
