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
        List<FloodParcelEntity> parcels = new List<FloodParcelEntity>();

        targetArea = await repoFlap.SaveFlapTargetAreaAsync(targetArea);

        var pamsPins = request.PamsPins ?? new List<string>();

        foreach (var item in pamsPins)
        {
            parcels.Add(new FloodParcelEntity() { PamsPin = item });
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            if(parcels.Count() > 0)
            {

                await repoParcel.LinkTargetAreaIdToParcelAsync(parcels, targetArea.Id);
            }


            scope.Complete();
        }
        return targetArea.Id;
    }
}
