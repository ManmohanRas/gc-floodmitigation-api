namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDeclarationCommandHandler : BaseHandler, IRequestHandler<SaveDeclarationCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IApplicationUserRepository repoApplicationUser;
    public SaveDeclarationCommandHandler (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoApplicationParcel,
        IApplicationUserRepository repoApplicationUser
        ) : base (repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoApplicationUser = repoApplicationUser;
    }
    public async Task<bool> Handle(SaveDeclarationCommand request, CancellationToken cancellationToken)
    {
        bool result = false;

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        var reqApp = mapper.Map<SaveDeclarationCommand, FloodApplicationEntity>(request);
        application.Title = reqApp.Title;
        application.AgencyId = reqApp.AgencyId;
        application.ApplicationSubTypeId = reqApp.ApplicationSubTypeId;

        //update application parcels
        var reqAppParcels = request.PamsPins.Select(o => new FloodApplicationParcelEntity() {
            ApplicationId = application.Id,
            PamsPin = o,
            IsLocked = false
        }).ToList();

        //update application users
        var reqAppUsers = mapper.Map<List<FloodApplicationUserViewModel>, List<FloodApplicationUserEntity>>(request.ApplicationUsers);
        reqAppUsers = reqAppUsers.Select(o => {
            o.ApplicationId = application.Id;
            o.LastUpdatedBy = userContext.Email;
            return o;
        }).Where(au => (au.IsPrimaryContact) || (au.IsAlternateContact)).ToList();

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplication.SaveAsync(application);
            await repoApplicationParcel.DeleteApplicationParcelsByApplicationIdAsync(application.Id);
            await repoApplicationParcel.SaveAsync(reqAppParcels);
            await repoApplicationUser.DeleteApplicationUsersByApplicationIdAsync(application.Id);
            await repoApplicationUser.SaveAsync(reqAppUsers);

            scope.Complete();
            result = true;
        }

        return result;
    }
}
