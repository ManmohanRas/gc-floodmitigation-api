namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsQueryHandler : BaseHandler, IRequestHandler<GetApplicationsQuery, IEnumerable<GetApplicationsQueryViewModel>>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly ICoreRepository repoCore;
    private readonly IApplicationRepository repoApplication;

    public GetApplicationsQueryHandler(
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
    public async Task<IEnumerable<GetApplicationsQueryViewModel>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = await this.repoApplication.GetApplicationsByAgenciesAsync(userContext.AgencyIds, userContext.IsExternalUser);
        var results = mapper.Map<IEnumerable<FloodApplicationEntity>, IEnumerable<GetApplicationsQueryViewModel>>(applications);
        return results;
    }
}
