namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTechDetailsQueryHandler : BaseHandler, IRequestHandler<GetTechDetailsQuery, GetTechDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private ITechDetailsRepository repoTech;
    private IParcelRepository repoProperty;
    private readonly IPresTrustUserContext userContext;
    public GetTechDetailsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationRepository repoApplication,
        ITechDetailsRepository repoTech,
        IParcelRepository repoProperty
       ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoApplication = repoApplication;
        this.repoTech = repoTech;
        this.repoProperty = repoProperty;
    }
    public async Task<GetTechDetailsQueryViewModel> Handle(GetTechDetailsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        // get tech details
        var tech = await repoTech.GetTechAsync(request.ApplicationId, request.PamsPin);
        // get recently locked details if there is no data
        if (!(tech.Id > 0))
        {
            var property = await repoProperty.GetParcelAsync(application.Id, request.PamsPin);
            if (property?.LockedAnotherApplicationId > 0)
            {
                tech = await repoTech.GetTechAsync(property.LockedAnotherApplicationId.Value, request.PamsPin);
                tech.Id = 0;
                tech.ApplicationId = application.Id;
            }
        }
        var result = mapper.Map<FloodTechDetailsEntity, GetTechDetailsQueryViewModel>(tech);

        return result;
    }
}
