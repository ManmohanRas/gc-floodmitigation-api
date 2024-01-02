namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapTargetAreaCommandHandler: IRequestHandler<SaveFlapTargetAreaCommand, int>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private IFlapModuleRepository repoFlap;
    private IParcelRepository repoParcel;

    public SaveFlapTargetAreaCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IFlapModuleRepository repoFlap,
        IParcelRepository repoParcel
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoFlap = repoFlap;
        this.repoParcel = repoParcel;
    }

    public async Task<int> Handle(SaveFlapTargetAreaCommand request, CancellationToken cancellationToken)
    {
        var targetArea = mapper.Map<SaveFlapTargetAreaCommand, FloodFlapTargetAreaEntity>(request);
        targetArea.LastUpdatedBy = userContext.Email;

        targetArea = await repoFlap.SaveFlapTargetAreaAsync(targetArea);

        var parcelIds = request.ParcelIds ?? new List<int>();
               
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            if(parcelIds.Count() > 0)
                await repoParcel.LinkTargetAreaIdToParcelAsync(parcelIds, targetArea.Id);

            scope.Complete();
        }
        return targetArea.Id;
    }
}
