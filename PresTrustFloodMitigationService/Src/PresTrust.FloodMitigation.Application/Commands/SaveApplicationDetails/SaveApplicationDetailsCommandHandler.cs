namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationDetailsCommandHandler : BaseHandler, IRequestHandler<SaveApplicationDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationDetailsRepository appDetails;

    public SaveApplicationDetailsCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationReleaseOfFundsRepository repoROF
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.appDetails = appDetails;
    }

    public async Task<int> Handle(SaveApplicationDetailsCommand request, CancellationToken cancellationToken)
    {
        int AppDetailsId = 0;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodApplicationDetailsEntity
        var reqAppDetails = mapper.Map<SaveApplicationDetailsCommand, FloodApplicationDetailsEntity>(request);

        var AppDetails = await appDetails.SaveAsync(reqAppDetails);

        AppDetailsId = AppDetails.Id;

        return AppDetailsId;

    }

}
