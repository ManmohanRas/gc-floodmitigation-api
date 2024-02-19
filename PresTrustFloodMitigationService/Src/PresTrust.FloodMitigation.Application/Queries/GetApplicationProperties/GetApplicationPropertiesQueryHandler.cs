namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationPropertiesQueryHandler : IRequestHandler<GetApplicationPropertiesQuery, IEnumerable<GetApplicationPropertiesQueryViewModel>>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoApplicationParcel;

    public GetApplicationPropertiesQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationParcelRepository repoApplicationParcel
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplicationParcel = repoApplicationParcel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetApplicationPropertiesQueryViewModel>> Handle(GetApplicationPropertiesQuery request, CancellationToken cancellationToken)
    {
        var parcels = await repoApplicationParcel.GetApplicationPropertiesAsync(request.ApplicationId);
        var result = mapper.Map<IEnumerable<FloodParcelEntity>, IEnumerable<GetApplicationPropertiesQueryViewModel>>(parcels);
        result = result.OrderBy(o => o.PamsPin);
        return result;
    }
}
