using Microsoft.AspNetCore.Builder;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class WithdrawApplicationCommandHandler : BaseHandler, IRequestHandler<WithdrawApplicationCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IBrokenRuleRepository repoApplicationBrokenRule;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRule;
    private readonly IEmailManager repoEmailManager;

    public WithdrawApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IBrokenRuleRepository repoApplicationBrokenRule,
        IPropertyBrokenRuleRepository repoPropertyBrokenRule,
        IEmailManager repoEmailManager
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoApplicationBrokenRule = repoApplicationBrokenRule;
        this.repoPropertyBrokenRule = repoPropertyBrokenRule;
        this.repoEmailManager = repoEmailManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Unit> Handle(WithdrawApplicationCommand request, CancellationToken cancellationToken)
    {
        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.WITHDRAWN;
            application.LastUpdatedBy = userContext.Email;
        }

        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
        appParcels = appParcels.Where(o => o.StatusId != (int)PropertyStatusEnum.TRANSFERRED).ToList();

        //update application parcels
        foreach (var appParcel in appParcels)
        {
            appParcel.StatusId = (int)PropertyStatusEnum.WITHDRAWN;
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
            await repoEmailManager.GetEmailTemplate(EmailTemplateCodeTypeEnum.CHANGE_STATUS_FROM_ACTIVE_TO_WITHDRAWN.ToString(), application);

            scope.Complete();
        }

        return Unit.Value;
    }
}
