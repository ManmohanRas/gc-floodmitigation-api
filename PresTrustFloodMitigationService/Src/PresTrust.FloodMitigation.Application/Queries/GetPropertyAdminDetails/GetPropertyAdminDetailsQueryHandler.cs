using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyAdminDetailsQueryHandler : BaseHandler, IRequestHandler<GetPropertyAdminDetailsQuery, GetPropertyAdminDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IPropertyAdminDetailsRepository PropDetails;

    public GetPropertyAdminDetailsQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IPropertyAdminDetailsRepository PropDetails
        )
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.PropDetails = PropDetails;
    }

    public async Task<GetPropertyAdminDetailsQueryViewModel> Handle(GetPropertyAdminDetailsQuery request, CancellationToken cancellationToken)
    {
        var propertyDetails = await PropDetails.GetAsync(request.ApplicationId, request.PamsPin);

        var result = mapper.Map<FloodPropertyAdminDetailsEntity, GetPropertyAdminDetailsQueryViewModel>(propertyDetails);

        return result;
    }
}
