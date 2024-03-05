namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFloodParcelsByFilterQueryHandler : IRequestHandler<GetFloodParcelsByFilterQuery, IEnumerable<GetFloodParcelsByFilterQueryViewModel>>
{

    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly ICoreRepository repoCore;

    public GetFloodParcelsByFilterQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        ICoreRepository repoCore
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoCore = repoCore;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetFloodParcelsByFilterQueryViewModel>> Handle(GetFloodParcelsByFilterQuery request, CancellationToken cancellationToken)
    {
        var parcels = await repoCore.GetFloodParcelsByFilterAsync(request.AgencyId, request.Block, request.Lot, request.Address, request.ExistingPamsPins, request.IsDOI);
        var result = mapper.Map<IEnumerable<FloodParcelEntity>, IEnumerable<GetFloodParcelsByFilterQueryViewModel>>(parcels);
        return result;
    }
}
