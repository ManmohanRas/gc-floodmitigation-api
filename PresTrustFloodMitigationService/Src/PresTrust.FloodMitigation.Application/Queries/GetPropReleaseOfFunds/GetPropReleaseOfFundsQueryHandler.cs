using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropReleaseOfFundsQueryHandler : BaseHandler, IRequestHandler<GetPropReleaseOfFundsQuery, GetPropReleaseOfFundsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IPropReleaseOfFundsRepository repoGrant;
    private readonly IPresTrustUserContext userContext;

    public GetPropReleaseOfFundsQueryHandler(
            IMapper mapper,
            IPresTrustUserContext userContext,
            IApplicationRepository repoApplication,
            IPropReleaseOfFundsRepository repoGrant
        )
    {
        this.mapper = mapper;
        this.userContext = userContext; 
        this.repoApplication = repoApplication;
        this.repoGrant = repoGrant;
    }

    public async Task<GetPropReleaseOfFundsQueryViewModel> Handle(GetPropReleaseOfFundsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        var releaseOfFunds = await repoGrant.GetReleaseOfFundsAsync(request.ApplicationId, request.PamsPin);

        var result = mapper.Map<FloodPropReleaseOfFundsEntity, GetPropReleaseOfFundsQueryViewModel>(releaseOfFunds);

        return result;
    }

}
