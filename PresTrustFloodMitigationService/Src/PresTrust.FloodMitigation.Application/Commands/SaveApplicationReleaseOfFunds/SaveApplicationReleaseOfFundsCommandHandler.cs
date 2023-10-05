namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationReleaseOfFundsCommandHandler: BaseHandler, IRequestHandler<SaveApplicationReleaseOfFundsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationReleaseOfFundsRepository repoROF;

    public SaveApplicationReleaseOfFundsCommandHandler(
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
        this.repoROF = repoROF;
    }

    public async Task<int> Handle(SaveApplicationReleaseOfFundsCommand request, CancellationToken cancellationToken)
    {
        int releaseOfFundsId = 0;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodApplicationReleaseOfFundsEntity
        var reqReleaseOfFunds = mapper.Map<SaveApplicationReleaseOfFundsCommand, FloodApplicationReleaseOfFundsEntity>(request);

        var releaseOfFunds = await repoROF.SaveAsync(reqReleaseOfFunds);

        releaseOfFundsId = releaseOfFunds.Id;

        return releaseOfFundsId;

    }
}
