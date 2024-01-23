namespace PresTrust.FloodMitigation.Application.Commands;

public class ImportTargetListCommandHandler : IRequestHandler<ImportTargetListCommand, Unit>
{
    private readonly IFlapModuleRepository repoFlap;
    private readonly IParcelRepository repoParcels;
    private IMemoryCache _cache;
    private readonly SystemParameterConfiguration systemParamOptions;


    public ImportTargetListCommandHandler
        (
        IFlapModuleRepository repoFlap,
        IParcelRepository repoParcels,
        IMemoryCache _cache,
        IOptions<SystemParameterConfiguration> systemParamOptions
        )
    {
        this.repoFlap = repoFlap;
        this.repoParcels = repoParcels;
        this._cache = _cache ?? throw new ArgumentNullException(nameof(_cache));
        this.systemParamOptions = systemParamOptions.Value;
    }
    public async Task<Unit> Handle(ImportTargetListCommand request, CancellationToken cancellationToken)
    {
        List<FloodParcelEntity> importedParcels = new List<FloodParcelEntity>();
        List<string> pamsPins = new List<string>(); 

        var existingTargerAreas = await repoFlap.GetFlapTargetAreasAsync(request.AgencyId);

        //Get cache
        if (_cache.TryGetValue("ParcelsCache", out List<FloodParcelEntity> parcels))
        {
            importedParcels = parcels ?? new List<FloodParcelEntity>();
        }

        IEnumerable<IGrouping<string, FloodParcelEntity>> query =
                        importedParcels.GroupBy(x => x.TargetArea.ToString());

        foreach (IGrouping<string, FloodParcelEntity> parcel in query)
        {
            pamsPins = parcel.Select(y => y.PamsPin).ToList();
        }

        List<FloodFlapTargetAreaEntity> targetAreas = importedParcels.Select(o => new FloodFlapTargetAreaEntity()
        {
            AgencyId = o.AgencyId,
            TargetArea = o.TargetArea,
            CreatedDate = DateTime.Now,
        }).ToList();


        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await SaveTargetAreas(targetAreas, pamsPins);

            scope.Complete();
        }

        //Reset cache
        _cache.Remove("ParcelsCache");
            return Unit.Value;
    }

    //save target areas
    private async Task SaveTargetAreas(List<FloodFlapTargetAreaEntity> targetAreas, List<string> pamsPins)
    {
        foreach (var item in targetAreas)
        {
            FloodFlapTargetAreaEntity targetArea = await repoFlap.SaveFlapTargetAreaAsync(item);
            await repoParcels.LinkTargetAreaIdToParcelAsync(pamsPins, targetArea.Id);
        }
    }
}
