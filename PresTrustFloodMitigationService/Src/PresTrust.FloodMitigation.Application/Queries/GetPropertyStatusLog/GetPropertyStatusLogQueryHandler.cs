namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyStatusLogQueryHandler : BaseHandler, IRequestHandler<GetPropertyStatusLogQuery, IEnumerable<GetPropertyStatusLogQueryViewModel>>
{

    private IMapper mapper;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelRepository repoParcel;

    public GetPropertyStatusLogQueryHandler(
        IMapper mapper,
        IApplicationParcelRepository repoApplicationParcel,
        IApplicationRepository repoApplication,
        IParcelRepository repoParcel
        ) : base(repoApplication, repoApplicationParcel)
    {
        this.mapper = mapper;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoApplication = repoApplication;
        this.repoParcel = repoParcel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetPropertyStatusLogQueryViewModel>> Handle(GetPropertyStatusLogQuery request, CancellationToken cancellationToken)
    {
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);
        var parcelStatusLog = await repoParcel.GetParcelStatusLogAsync(application.Id, property.PamsPin);
        var results = mapper.Map<IEnumerable<FloodParcelStatusLogEntity>, IEnumerable<GetPropertyStatusLogQueryViewModel>>(parcelStatusLog);
        return results;
    }
}
