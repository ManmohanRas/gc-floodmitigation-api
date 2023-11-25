using Newtonsoft.Json.Linq;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;
public class SubmitPropertyCommandHandler : BaseHandler, IRequestHandler<SubmitPropertyCommand, SubmitPropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoPropBrokenRules;

    public SubmitPropertyCommandHandler
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
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SubmitPropertyCommandViewModel> Handle(SubmitPropertyCommand request, CancellationToken cancellationToken)
    {
        SubmitPropertyCommandViewModel result = new ();
        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        // check if application exists
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);
        // check if any broken rules exists, if yes then return
        var brokenRules = await repoPropBrokenRules.GetPropertyBrokenRulesAsync(property.ApplicationId, property.PamsPin);

        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<PropertyBrokenRulesViewModel>>(brokenRules);
            return result;
        }

        //update application
        if (property != null)
        {
            property.StatusId = (int)PropertyStatusEnum.SUBMITTED;
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

            // returns broken rules
            var defaultBrokenRules = ReturnBrokenRulesIfAny(application, property);
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
    private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity Application,FloodApplicationParcelEntity Property)
    {
        List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

        // add default broken rule while initiating application flow
        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        {
            ApplicationId = Application.Id,
            SectionId = (int)PropertySectionEnum.PROPERTY,
            PamsPin = Property.PamsPin,
            Message = "All required fields on Property tab have not been filled.",
            IsPropertyFlow = true
        });
        brokenRules.Add(new FloodPropertyBrokenRuleEntity()
        {
            ApplicationId = Application.Id,
            SectionId = (int)PropertySectionEnum.TECH,
            PamsPin = Property.PamsPin,
            Message = "All required fields on Tech tab have not been filled.",
            IsPropertyFlow = false
        });
        return brokenRules;
    }
}
