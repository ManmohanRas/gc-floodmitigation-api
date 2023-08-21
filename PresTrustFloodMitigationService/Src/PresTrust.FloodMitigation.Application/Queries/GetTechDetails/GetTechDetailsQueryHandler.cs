namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTechDetailsQueryHandler : BaseHandler, IRequestHandler<GetTechDetailsQuery, GetTechDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private ITechDetailsRepository repoTech;

    public GetTechDetailsQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        ITechDetailsRepository repoTech
       ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoTech = repoTech;
    }
    public async Task<GetTechDetailsQueryViewModel> Handle(GetTechDetailsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get signature details
        var Tech = await repoTech.GetTechAsync(request.ApplicationId);
        Tech = Tech ?? new FloodTechDetailsEntity()
        {
            ApplicationId = application.Id,
        };
        var result = mapper.Map<FloodTechDetailsEntity, GetTechDetailsQueryViewModel>(Tech);

        return result;
    }
}
