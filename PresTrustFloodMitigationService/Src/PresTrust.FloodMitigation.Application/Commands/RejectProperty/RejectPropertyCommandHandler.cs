using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;
public class RejectPropertyCommandHandler : BaseHandler, IRequestHandler<RejectPropertyCommand, RejectPropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRule;

    public RejectPropertyCommandHandler
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
    public async Task<RejectPropertyCommandViewModel> Handle(RejectPropertyCommand request, CancellationToken cancellationToken)
    {
        RejectPropertyCommandViewModel result = new ();

        // check if Property exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        //update Property
        if (Property != null)
        {
            Property.StatusId = (int)PropertyStatusEnum.REJECTED;
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
            await repoPropertyBrokenRule.DeleteAllPropertyBrokenRulesAsync(request.ApplicationId, request.PamsPin);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
