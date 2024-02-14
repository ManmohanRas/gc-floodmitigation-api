using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryItemCommandHandler: IRequestHandler<SaveParcelHistoryItemCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IParcelRepository repoParcel;
    private readonly IParcelHistoryRepository repoParcelHistory;

    public SaveParcelHistoryItemCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IParcelRepository repoParcel,
        IParcelHistoryRepository repoParcelHistory
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoParcel = repoParcel;
        this.repoParcelHistory = repoParcelHistory;
    }

    public async Task<int> Handle(SaveParcelHistoryItemCommand request, CancellationToken cancellationToken)
    {
        var parcel = await repoParcel.GetProgramManagerParcelAsync(request.ParcelId);
        parcel.PamsPin = request.CurrentPamsPin;
        parcel.Block = request.Block;
        parcel.Lot = request.Lot;
        parcel.QCode = request.QCode;

        var parcelHistoryItem = mapper.Map<SaveParcelHistoryItemCommand, FloodParcelHistoryEntity>(request);
        parcelHistoryItem.LastUpdatedBy = userContext.Name;
        
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoParcel.SaveProgramManagerParcelAsync(parcel);
            await repoParcelHistory.SaveParcelHistoryItemAsync(parcelHistoryItem);

            scope.Complete();
        }
        return parcelHistoryItem.Id;
    }
}
