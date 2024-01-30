using Newtonsoft.Json.Linq;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class RejectApplicationCommandHandler : BaseHandler, IRequestHandler<RejectApplicationCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IBrokenRuleRepository repoApplicationBrokenRule;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRule;

    public RejectApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IBrokenRuleRepository repoApplicationBrokenRule,
        IPropertyBrokenRuleRepository repoPropertyBrokenRule
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoApplicationBrokenRule = repoApplicationBrokenRule;
        this.repoPropertyBrokenRule = repoPropertyBrokenRule;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Unit> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
    {
        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.REJECTED;
            application.LastUpdatedBy = userContext.Email;
        }

        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);

        //update application parcels
        foreach (var appParcel in appParcels)
        {
            appParcel.StatusId = (int)PropertyStatusEnum.REJECTED;
            appParcel.IsLocked = true;
            appParcel.LastUpdatedBy = userContext.Email;
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
            await repoApplicationBrokenRule.DeleteAllBrokenRulesAsync(application.Id);

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
                await repoPropertyBrokenRule.DeleteAllPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin);

                await repoApplicationParcel.CreateLockedParcel(appParcel.ApplicationId, appParcel.PamsPin, userContext.Email);
            }

            scope.Complete();
        }

        return Unit.Value;
    }
}
