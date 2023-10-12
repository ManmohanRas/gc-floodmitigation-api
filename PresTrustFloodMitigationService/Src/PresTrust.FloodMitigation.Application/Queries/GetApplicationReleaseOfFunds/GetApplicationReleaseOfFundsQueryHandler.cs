namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationReleaseOfFundsQueryHandler: BaseHandler, IRequestHandler<GetApplicationReleaseOfFundsQuery, GetApplicationReleaseOfFundsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationReleaseOfFundsRepository repoApplicationROF;

    public GetApplicationReleaseOfFundsQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IApplicationReleaseOfFundsRepository repoApplicationROF
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoApplicationROF = repoApplicationROF;
    }

    public async Task<GetApplicationReleaseOfFundsQueryViewModel> Handle(GetApplicationReleaseOfFundsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var releaseOfFunds = await repoApplicationROF.GetReleaseOfFundsAsync(application.Id);

        var payments = await repoApplicationROF.GetApplicationPaymentsAsync(application.Id);

        var result = mapper.Map<FloodApplicationReleaseOfFundsEntity, GetApplicationReleaseOfFundsQueryViewModel>(releaseOfFunds);

        result.Payments = payments;

        return result;
    }
}
