using Newtonsoft.Json.Linq;

namespace PresTrust.FloodMitigation.Application.Commands;

public class ReinitiatePropertyCommandHandler : BaseHandler, IRequestHandler<ReinitiatePropertyCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;

    public ReinitiatePropertyCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel
    ) : base(repoApplication, repoApplicationParcel)
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
    public async Task<Unit> Handle(ReinitiatePropertyCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        // check if Property exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // get application parcels
        var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);

        FloodApplicationParcelEntity appParcel = appParcels.FirstOrDefault(o => o.PamsPin == Property.PamsPin);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            //set Status to Prev Status
            appParcel.StatusId = appParcel.PrevStatusId;

            await repoApplicationParcel.SaveApplicationParcelWorkflowStatusAsync(appParcel);
            FloodParcelStatusLogEntity appParcelStatusLog = new()
            {
                ApplicationId = appParcel.ApplicationId,
                PamsPin = request.PamsPin,
                StatusId = appParcel.StatusId,
                StatusDate = DateTime.Now,
                Notes = String.Empty,
                LastUpdatedBy = appParcel.LastUpdatedBy
            };
            await repoApplicationParcel.SaveStatusLogAsync(appParcelStatusLog);


            scope.Complete();
        }
        return Unit.Value;
    }
}
