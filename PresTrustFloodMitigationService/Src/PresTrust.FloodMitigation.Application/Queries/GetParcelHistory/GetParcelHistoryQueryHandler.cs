namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryQueryHandler: IRequestHandler<GetParcelHistoryQuery, IEnumerable<GetParcelHistoryQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IParcelHistoryRepository repoParcelHistory;
    private readonly IPresTrustUserContext userContext; 
    public GetParcelHistoryQueryHandler(
          IMapper mapper,
          IPresTrustUserContext userContext,
          IParcelHistoryRepository repoParcelHistory)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoParcelHistory = repoParcelHistory;
    }

    public async Task<IEnumerable<GetParcelHistoryQueryViewModel>> Handle(GetParcelHistoryQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodParcelHistoryEntity> results = default;
        results = await this.repoParcelHistory.GetParcelHistoryAsync(request.ParcelId);
        var parcelHistory = mapper.Map<IEnumerable<FloodParcelHistoryEntity>, IEnumerable<GetParcelHistoryQueryViewModel>>(results);
        return parcelHistory;
    }
}
