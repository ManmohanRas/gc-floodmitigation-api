namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationAdminDetailsCommandHandler : BaseHandler, IRequestHandler<SaveApplicationAdminDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationDetailsRepository repoAppDetails;

    public SaveApplicationAdminDetailsCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationDetailsRepository repoAppDetails
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoAppDetails = repoAppDetails;
    }

    public async Task<int> Handle(SaveApplicationAdminDetailsCommand request, CancellationToken cancellationToken)
    {
        int AppDetailsId = 0;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodApplicationDetailsEntity
        var reqAppDetails = mapper.Map<SaveApplicationAdminDetailsCommand, FloodApplicationAdminDetailsEntity>(request);

        var AppDetails = await repoAppDetails.SaveAsync(reqAppDetails);

        AppDetailsId = AppDetails.Id;

        return AppDetailsId;

    }

}
