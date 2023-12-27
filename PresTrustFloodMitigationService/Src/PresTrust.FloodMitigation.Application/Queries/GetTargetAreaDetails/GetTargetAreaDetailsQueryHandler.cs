namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTargetAreaDetailsQueryHandler : IRequestHandler<GetTargetAreaDetailsQuery, GetTargetAreaDetailsQueryViewModel>
{
    private IMapper mapper;
    private IFlapModuleRepository repoFlap;
    private IParcelRepository repoParcel;

    public GetTargetAreaDetailsQueryHandler
        (
        IMapper mapper,
        IFlapModuleRepository repoFlap,
        IParcelRepository repoParcel
        )
    {
        this.mapper = mapper;
        this.repoFlap = repoFlap;
        this.repoParcel = repoParcel;
    }
    public async Task<GetTargetAreaDetailsQueryViewModel> Handle(GetTargetAreaDetailsQuery request, CancellationToken cancellationToken)
    {
        //get target area details
        var reqTargetArea = await repoFlap.GetFlapTargetAreaByIdAsync(request.Id);
        var targetArea = mapper.Map<FloodFlapTargetAreaEntity, GetTargetAreaDetailsQueryViewModel>(reqTargetArea);

        var reqParcels = await repoParcel.GetParcelsByTargetAreaIdAsync(targetArea.Id);
        var parcels = mapper.Map<IEnumerable<FloodParcelEntity>, IEnumerable<FloodFlapParcelViewModel>>(reqParcels);
        
        targetArea.Parcels = parcels;

        return targetArea;
    }
}
