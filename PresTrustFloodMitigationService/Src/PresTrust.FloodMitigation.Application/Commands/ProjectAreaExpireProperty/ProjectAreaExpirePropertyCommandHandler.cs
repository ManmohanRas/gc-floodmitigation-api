using MediatR;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class ProjectAreaExpirePropertyCommandHandler : BaseHandler, IRequestHandler<ProjectAreaExpirePropertyCommand, ProjectAreaExpirePropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRule;

    public ProjectAreaExpirePropertyCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoProperty,
        IPropertyBrokenRuleRepository repoPropertyBrokenRule
    ) : base(repoApplication, repoProperty)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoProperty = repoProperty;
        this.repoPropertyBrokenRule = repoPropertyBrokenRule;
    }

    /// <summary>
    /// 
    /// </summary>....................... 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProjectAreaExpirePropertyCommandViewModel> Handle(ProjectAreaExpirePropertyCommand request, CancellationToken cancellationToken)
    {
        ProjectAreaExpirePropertyCommandViewModel result = new ();

        // check if Property exists
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        //update Property
        if (property != null)
        {
            property.StatusId = (int)PropertyStatusEnum.PROJECT_AREA_EXPIRED;
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
            await repoPropertyBrokenRule.DeleteAllPropertyBrokenRulesAsync(request.ApplicationId, request.PamsPin);

            await repoProperty.CreateLockedParcel(property.ApplicationId, property.PamsPin, userContext.Email);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
