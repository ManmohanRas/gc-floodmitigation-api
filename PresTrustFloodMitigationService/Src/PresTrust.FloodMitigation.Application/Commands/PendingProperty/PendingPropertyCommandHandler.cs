using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class PendingPropertyCommandHandler : BaseHandler, IRequestHandler<PendingPropertyCommand, PendingPropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoPropBrokenRules;
    public PendingPropertyCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoProperty,
        IPropertyBrokenRuleRepository repoPropBrokenRules
    ) : base(repoApplication, repoProperty)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoProperty = repoProperty;
        this.repoPropBrokenRules = repoPropBrokenRules;
    }

    /// <summary>
    /// 
    /// </summary>....................... 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PendingPropertyCommandViewModel> Handle(PendingPropertyCommand request, CancellationToken cancellationToken)
    {
        PendingPropertyCommandViewModel result = new ();

        // check if application exists
        var Application = await GetIfApplicationExists(request.ApplicationId);
        
        // check if Property exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);
        // check if any broken rules exists, if yes then return
        var brokenRules = await repoPropBrokenRules.GetPropertyBrokenRulesAsync(Property.ApplicationId, Property.PamsPin);

        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<PropertyBrokenRulesViewModel>>(brokenRules);
            return result;
        }

        //update Property
        if (Property != null)
        {
            Property.StatusId = (int)PropertyStatusEnum.PENDING;
            Property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(Property);
            FloodParcelStatusLogEntity appParcelStatusLog = new()
            {
                ApplicationId = Property.ApplicationId,
                PamsPin = Property.PamsPin,
                StatusId = Property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = Property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appParcelStatusLog);
            // returns broken rules  
            var defaultBrokenRules = ReturnBrokenRulesIfAny(Application, Property);
            // Save current Broken Rules, if any
            await repoPropBrokenRules.SavePropertyBrokenRules(defaultBrokenRules);
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
    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity Application, FloodApplicationParcelEntity Property)
    {
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

        // add default broken rule while initiating application flow
        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        {
            ApplicationId = Application.Id,
            SectionId = (int)PropertySectionEnum.ADMIN_DETAILS,
            PamsPin = Property.PamsPin,
            Message = "All required fields on ADMIN_DETAILS Tab have not been filled.",
            IsPropertyFlow = false
        }); 
        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        {
            ApplicationId = Application.Id,
            SectionId = (int)PropertySectionEnum.FINANCE,
            PamsPin = Property.PamsPin,
            Message = "All required fields on FINANCE tab have not been filled.",
            IsPropertyFlow = false
        }); 
        //brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        //{
        //    ApplicationId = Application.Id,
        //    SectionId = (int)PropertySectionEnum.ADMIN_DOCUMENT_CHECKLIST,
        //    PamsPin = Property.PamsPin,
        //    Message = "All required fields on Property tab have not been filled.",
        //    IsPropertyFlow = false
        //});

        return brokenRules;
    }
}
