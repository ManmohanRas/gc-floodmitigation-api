namespace PresTrust.FloodMitigation.Application.Queries;
public class GetParcelHistoryItemQueryHandler: IRequestHandler<GetParcelHistoryItemQuery, GetParcelHistoryItemQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IParcelHistoryRepository repoParcelHistory;
    public GetParcelHistoryItemQueryHandler(
          IMapper mapper,
          IParcelHistoryRepository repoParcelHistory)
    {
        this.mapper = mapper;
        this.repoParcelHistory = repoParcelHistory;
    }

    public async Task<GetParcelHistoryItemQueryViewModel> Handle(GetParcelHistoryItemQuery request, CancellationToken cancellationToken)
    {
        FloodParcelHistoryEntity result = default;
        result = await this.repoParcelHistory.GetParcelHistoryItemAsync(request.ParcelId);
        var parcelHistoryItem = mapper.Map<FloodParcelHistoryEntity, GetParcelHistoryItemQueryViewModel>(result);
        return parcelHistoryItem;
    }
}
