using Newtonsoft.Json.Linq;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class TransferPropertyCommandHandler : BaseHandler, IRequestHandler<TransferPropertyCommand, TransferPropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRule;

    public TransferPropertyCommandHandler
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
        this.repoPropertyBrokenRule= repoPropertyBrokenRule;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TransferPropertyCommandViewModel> Handle(TransferPropertyCommand request, CancellationToken cancellationToken)
    {
        TransferPropertyCommandViewModel result = new();

        // check if application exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        //update application
        if (Property != null)
        {
            Property.StatusId = (int)PropertyStatusEnum.TRANSFERRED;
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
