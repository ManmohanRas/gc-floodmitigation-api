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
        ) : base(repoApplication: repoApplication)
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
        var result = mapper.Map<FloodApplicationAdminDetailsEntity, GetApplicationAdminDetailsQueryViewModel>(details);
        return result;
    }
}