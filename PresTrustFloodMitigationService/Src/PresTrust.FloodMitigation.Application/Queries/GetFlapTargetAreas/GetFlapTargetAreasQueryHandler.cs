namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapTargetAreasQueryHandler : IRequestHandler<GetFlapTargetAreasQuery, IEnumerable<GetFlapTargetAreasQueryViewModel>>
{
    private IMapper mapper;
    private IFlapModuleRepository repoFlap;

    public GetFlapTargetAreasQueryHandler
        (
        IMapper mapper,
        IFlapModuleRepository repoFlap
        )
    {
        this.mapper = mapper;
        this.repoFlap = repoFlap;
    }
    public async Task<IEnumerable<GetFlapTargetAreasQueryViewModel>> Handle(GetFlapTargetAreasQuery request, CancellationToken cancellationToken)
    {
        //get target areas
        var reqTargetAreas = await repoFlap.GetFlapTargetAreasAsync(request.AgencyId);
        var targetAreas = mapper.Map<IEnumerable<FloodFlapTargetAreaEntity>, IEnumerable<GetFlapTargetAreasQueryViewModel>>(reqTargetAreas);

        return targetAreas;
    }
}
