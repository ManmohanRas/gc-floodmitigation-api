namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelListQueryHandler : BaseHandler, IRequestHandler<GetParcelListQuery , IEnumerable<GetParcelListQueryViewModel>>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly ICoreRepository repoCore;
    private readonly IParcelRepository repoApplication;

    public GetParcelListQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        ICoreRepository repoCore,
        IParcelRepository repoApplication
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
    public async Task<IEnumerable<GetParcelListQueryViewModel>> Handle(GetParcelListQuery request, CancellationToken cancellationToken)
    {
        var parcels = await this.repoApplication.GetParcelListAsync();
        var results = mapper.Map<IEnumerable<FloodParcelListEntity>, IEnumerable<GetParcelListQueryViewModel>>(parcels);
        results = results.OrderBy(o => o.PamsPin).ThenBy(o => o.ProjectArea);
        return results;
    }
}
