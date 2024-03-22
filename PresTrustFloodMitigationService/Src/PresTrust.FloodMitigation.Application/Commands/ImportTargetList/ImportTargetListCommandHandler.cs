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
        FloodFlapTargetAreaEntity targetArea = new FloodFlapTargetAreaEntity();

        var existingTargerAreas = await repoFlap.GetFlapTargetAreasAsync(request.AgencyId);

        //Get cache
        if (_cache.TryGetValue("ParcelsCache", out List<FloodParcelEntity> parcels))
        {
            importedParcels = parcels ?? new List<FloodParcelEntity>();
        }

        IEnumerable<IGrouping<string, FloodParcelEntity>> query =
                        importedParcels.GroupBy(x => x.TargetArea.ToString());

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            //delink parcels
            await repoParcels.DelinkParcelfromTargetArea(importedParcels.Select(x => x.PamsPin).ToList());

            foreach (IGrouping<string, FloodParcelEntity> parcel in query)
            {
                targetArea = new FloodFlapTargetAreaEntity()
                {
                    AgencyId = request.AgencyId,
                    Id = existingTargerAreas.Where(x => x.TargetArea == parcel.Key).Select(o => o.Id).FirstOrDefault(),
                    TargetArea = parcel.Key,
                    CreatedDate = DateTime.Now,
                };
                pamsPins = parcel.Select(y => y.PamsPin).ToList();
                await SaveTargetAreas(targetArea, importedParcels);
            }

            scope.Complete();
        }

        //Reset cache
        _cache.Remove("ParcelsCache");
            return Unit.Value;
    }

    //save target areas
    private async Task SaveTargetAreas(FloodFlapTargetAreaEntity targetArea, List<FloodParcelEntity> parcels)
    {
        if (targetArea.Id == 0)
        {
            targetArea = await repoFlap.SaveFlapTargetAreaAsync(targetArea);
        }

        await repoParcels.LinkTargetAreaIdToParcelAsync(parcels, targetArea.Id, true);

    }
}
