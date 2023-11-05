namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelTrackingQueryHandler : BaseHandler, IRequestHandler<GetParcelTrackingQuery, GetParcelTrackingQueryViewModel>
{
    private IMapper mapper;
    private IApplicationRepository repoApplication;
    private IParcelTrackingRepository repoParcelTracking;
    public GetParcelTrackingQueryHandler(
        IMapper mapper
       , IApplicationRepository repoApplication
       , IParcelTrackingRepository repoParcelTracking
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoParcelTracking = repoParcelTracking;
    }

    public async Task<GetParcelTrackingQueryViewModel> Handle(GetParcelTrackingQuery request, CancellationToken cancellationToken)
    {
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get parcel Tracking
        var parcelTracking = await this.repoParcelTracking.GetTrackingAsync(request.ApplicationId, request.PamsPin);

        var result = mapper.Map<FloodParcelTrackingEntity, GetParcelTrackingQueryViewModel>(parcelTracking);

        return result;
    }
}
