namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsForWarningsQueryHandler : BaseHandler, IRequestHandler<GetApplicationsForWarningsQuery, IEnumerable<GetApplicationsForWarningsQueryViewModel>>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly ICoreRepository repoCore;
    private readonly IApplicationRepository repoApplication;

    public GetApplicationsForWarningsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        ICoreRepository repoCore,
        IApplicationRepository repoApplication
        ) : base()
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoCore = repoCore;
        this.repoApplication = repoApplication;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetApplicationsForWarningsQueryViewModel>> Handle(GetApplicationsForWarningsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var applications = await this.repoApplication.GetApplicationsForWarningsAsync(request.ApplicationIds, request.PamsPin, request.IsTransfer);
        var results = mapper.Map<IEnumerable<FloodApplicationEntity>, IEnumerable<GetApplicationsForWarningsQueryViewModel>>(applications);
        return results;
    }
}
