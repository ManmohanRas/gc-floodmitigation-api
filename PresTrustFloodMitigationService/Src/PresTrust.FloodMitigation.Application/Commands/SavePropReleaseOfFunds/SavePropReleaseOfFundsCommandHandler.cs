using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropReleaseOfFundsCommandHandler : BaseHandler, IRequestHandler<SavePropReleaseOfFundsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private IPropReleaseOfFundsRepository repoPropReleaseOfFunds;

    public SavePropReleaseOfFundsCommandHandler
    (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       IPropReleaseOfFundsRepository repoPropReleaseOfFunds
    )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoPropReleaseOfFunds = repoPropReleaseOfFunds;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SavePropReleaseOfFundsCommand request, CancellationToken cancellationToken)
    {
        // map command object to the FloodTechDetailsEntity
        var reqPropRof = mapper.Map<SavePropReleaseOfFundsCommand, FloodPropReleaseOfFundsEntity>(request);
        reqPropRof.LastUpdatedBy = userContext.Email;

        var propReleaseOfFunds = await repoPropReleaseOfFunds.SaveAsync(reqPropRof);
        return propReleaseOfFunds.Id;
    }

}
