namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelPropertyQueryHandler : BaseHandler, IRequestHandler<GetParcelPropertyQuery, GetParcelPropertyQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IParcelPropertyRepository repoProperty;

    public GetParcelPropertyQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IParcelPropertyRepository repoProperty
       ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoProperty = repoProperty;
    }
    public async Task<GetParcelPropertyQueryViewModel> Handle(GetParcelPropertyQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get tech details
        var Property = await repoProperty.GetAsync(request.ApplicationId, request.PamsPin);
        var result = mapper.Map<FloodParcelPropertyEntity, GetParcelPropertyQueryViewModel>(Property);

        return result;
    }
}
