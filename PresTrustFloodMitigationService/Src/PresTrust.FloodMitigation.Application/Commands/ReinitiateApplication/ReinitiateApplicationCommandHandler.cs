using Microsoft.AspNetCore.Builder;

namespace PresTrust.FloodMitigation.Application.Commands;
public class ReinitiateApplicationCommandHandler : BaseHandler, IRequestHandler<ReinitiateApplicationCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;

    public ReinitiateApplicationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Unit> Handle(ReinitiateApplicationCommand request, CancellationToken cancellationToken)
    {
        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        if (application != null)
        {
            application.StatusId = application.PrevStatusId;
            application.LastUpdatedBy = userContext.Email;
        }


        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
        appParcels = appParcels.Where(o => o.StatusId != (int)PropertyStatusEnum.TRANSFERRED).ToList();

        //update application parcels
        foreach (var appParcel in appParcels)
        {
            appParcel.StatusId = appParcel.PrevStatusId;
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

            scope.Complete();
        }

        return Unit.Value;
    }
}
