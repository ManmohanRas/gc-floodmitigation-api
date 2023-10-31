namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationStatusLogQueryHandler : BaseHandler, IRequestHandler<GetApplicationStatusLogQuery, IEnumerable<GetApplicationStatusLogQueryViewModel>>
{

    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;

    public GetApplicationStatusLogQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication
        ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetApplicationStatusLogQueryViewModel>> Handle(GetApplicationStatusLogQuery request, CancellationToken cancellationToken)
    {
        var application = await GetIfApplicationExists(request.ApplicationId);
        var applicationStatusLog = await this.repoApplication.GetApplicationStatusLogAsync(application.Id);
        var results = mapper.Map<IEnumerable<FloodApplicationStatusLogEntity>, IEnumerable<GetApplicationStatusLogQueryViewModel>>(applicationStatusLog);
        return results;
    }
}
