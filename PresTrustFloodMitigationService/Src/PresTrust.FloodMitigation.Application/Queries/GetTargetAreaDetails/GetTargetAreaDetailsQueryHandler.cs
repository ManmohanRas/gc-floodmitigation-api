namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTargetAreaDetailsQueryHandler : IRequestHandler<GetTargetAreaDetailsQuery, GetTargetAreaDetailsQueryViewModel>
{
    private IMapper mapper;
    private IFlapModuleRepository repoFlap;
    private IParcelRepository repoParcel;
    private IPresTrustUserContext userContext;

    public GetTargetAreaDetailsQueryHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IFlapModuleRepository repoFlap,
        IParcelRepository repoParcel
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFlap = repoFlap;
        this.repoParcel = repoParcel;
    }
    public async Task<GetTargetAreaDetailsQueryViewModel> Handle(GetTargetAreaDetailsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        //get target area details
        var reqTargetArea = await repoFlap.GetFlapTargetAreaByIdAsync(request.Id);
        var targetArea = mapper.Map<FloodFlapTargetAreaEntity, GetTargetAreaDetailsQueryViewModel>(reqTargetArea);

        var reqParcels = await repoParcel.GetParcelsByTargetAreaIdAsync(targetArea.Id);
        var parcels = mapper.Map<IEnumerable<FloodParcelEntity>, IEnumerable<GetFloodFlapParcelViewModel>>(reqParcels);
        
        targetArea.Parcels = parcels;

        return targetArea;
    }
}
