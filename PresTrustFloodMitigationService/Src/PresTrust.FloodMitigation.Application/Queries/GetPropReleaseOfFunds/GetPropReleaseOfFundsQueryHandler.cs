using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropReleaseOfFundsQueryHandler : BaseHandler, IRequestHandler<GetPropReleaseOfFundsQuery, GetPropReleaseOfFundsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IPropReleaseOfFundsRepository repoGrant;

    public GetPropReleaseOfFundsQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IPropReleaseOfFundsRepository repoGrant
        )
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoGrant = repoGrant;
    }

    public async Task<GetPropReleaseOfFundsQueryViewModel> Handle(GetPropReleaseOfFundsQuery request, CancellationToken cancellationToken)
    {
        var releaseOfFunds = await repoGrant.GetReleaseOfFundsAsync(request.ApplicationId, request.Pamspin);

        releaseOfFunds = releaseOfFunds ?? new FloodPropReleaseOfFundsEntity()
        {
            ApplicationId = request.ApplicationId,
        };

        var result = mapper.Map<FloodPropReleaseOfFundsEntity, GetPropReleaseOfFundsQueryViewModel>(releaseOfFunds);

        return result;
    }

}
