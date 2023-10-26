namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationAdminDetailsQueryHandler : BaseHandler, IRequestHandler<GetApplicationAdminDetailsQuery, GetApplicationAdminDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IApplicationDetailsRepository repoDetails;
    public GetApplicationAdminDetailsQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IApplicationDetailsRepository repoDetails
        )
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoDetails = repoDetails;
    }

    public async Task<GetApplicationAdminDetailsQueryViewModel> Handle(GetApplicationAdminDetailsQuery request, CancellationToken cancellationToken)
    {
        //get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        //get Admin details
        var details = await this.repoDetails.GetAsync(request.ApplicationId);
        details = details ?? new FloodApplicationDetailsEntity()
        {
            ApplicationId = application.Id
        };
        var result = mapper.Map<FloodApplicationDetailsEntity, GetApplicationAdminDetailsQueryViewModel>(details);
        return result;
    }
}