namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelPropertyQueryHandler : BaseHandler, IRequestHandler<GetParcelPropertyQuery, GetParcelPropertyQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IParcelPropertyRepository repoParcel;
    private IParcelRepository repoProperty;

    public GetParcelPropertyQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IParcelPropertyRepository repoParcel,
        IParcelRepository repoProperty
       ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoParcel = repoParcel;
        this.repoProperty = repoProperty;
    }
    public async Task<GetParcelPropertyQueryViewModel> Handle(GetParcelPropertyQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        // get parcel details
        var parcel = await repoParcel.GetAsync(request.ApplicationId, request.PamsPin);
        // get recently locked details if there is no data
        if (!(parcel.Id > 0))
        {
            var property = await repoProperty.GetParcelAsync(application.Id, request.PamsPin);
            if(property?.LockedAnotherApplicationId > 0)
            {
                parcel = await repoParcel.GetAsync(property.LockedAnotherApplicationId.Value, request.PamsPin);
                parcel.Id = 0;
                parcel.ApplicationId = application.Id;
            }
        }
        var result = mapper.Map<FloodParcelPropertyEntity, GetParcelPropertyQueryViewModel>(parcel);

        return result;
    }
}
