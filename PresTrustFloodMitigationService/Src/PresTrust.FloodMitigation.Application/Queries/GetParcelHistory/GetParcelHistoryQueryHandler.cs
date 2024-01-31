namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryQueryHandler: IRequestHandler<GetParcelHistoryQuery, IEnumerable<GetParcelHistoryQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IParcelHistoryRepository repoParcelHistory;
    public GetParcelHistoryQueryHandler(
          IMapper mapper,
          IParcelHistoryRepository repoParcelHistory)
    {
        this.mapper = mapper;
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
