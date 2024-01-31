namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryCommandHandler: IRequestHandler<SaveParcelHistoryCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IParcelHistoryRepository repoParcelHistory;

    public SaveParcelHistoryCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IParcelHistoryRepository repoParcelHistory
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoParcelHistory = repoParcelHistory;
    }

    public async Task<int> Handle(SaveParcelHistoryCommand request, CancellationToken cancellationToken)
    {
        var reqParcelHistory = mapper.Map<SaveParcelHistoryCommand, FloodParcelHistoryEntity>(request);
        reqParcelHistory.LastUpdatedBy = this.userContext.Name;
        var parcelHistoryItem = await this.repoParcelHistory.SaveParcelHistoryAsync(reqParcelHistory);
        return parcelHistoryItem.Id;
    }
}
