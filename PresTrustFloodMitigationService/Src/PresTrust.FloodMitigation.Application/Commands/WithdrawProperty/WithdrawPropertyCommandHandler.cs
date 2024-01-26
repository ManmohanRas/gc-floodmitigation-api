using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;
public class WithdrawPropertyCommandHandler : BaseHandler, IRequestHandler<WithdrawPropertyCommand, WithdrawPropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRule;

    public WithdrawPropertyCommandHandler
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
    public async Task<WithdrawPropertyCommandViewModel> Handle(WithdrawPropertyCommand request, CancellationToken cancellationToken)
    {
        WithdrawPropertyCommandViewModel result = new ();

        // check if Property exists
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        //update Property
        if (property != null)
        {
            property.StatusId = (int)PropertyStatusEnum.WITHDRAWN;
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

            await repoProperty.CreateLockedParcel();

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
