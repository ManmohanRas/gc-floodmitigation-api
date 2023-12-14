using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapDetailsQueryHandler : IRequestHandler<GetFlapDetailsQuery, GetFlapDetailsQueryViewModel>
{
    private IMapper mapper;
    private IFlapModuleRepository repoFlap;

    public GetFlapDetailsQueryHandler(
        IMapper mapper,
        IFlapModuleRepository repoFlap
        )
    {
        this.mapper = mapper;
        this.repoFlap = repoFlap;
    }
    public async Task<GetFlapDetailsQueryViewModel> Handle(GetFlapDetailsQuery request, CancellationToken cancellationToken)
    {
        //get flap details
        var reqFlap = await repoFlap.GetFlapAsync(request.AgencyId);
        var flapComments = await repoFlap.GetFlapCommentsAsync(request.AgencyId);

        var flap = mapper.Map<FloodFlapEntity, GetFlapDetailsQueryViewModel>(reqFlap);
        flap.FlapComments = flapComments;

        return flap;

    }
}
