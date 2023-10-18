namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleaseApplicationPaymentsCommandHandler: BaseHandler, IRequestHandler<ReleaseApplicationPaymentsCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    //private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationReleaseOfFundsRepository repoROF;

    public ReleaseApplicationPaymentsCommandHandler(
        IMapper mapper, IPresTrustUserContext userContext, 
        //SystemParameterConfiguration systemParamOptions, 
        IApplicationRepository repoApplication,
        IApplicationReleaseOfFundsRepository repoROF
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        //this.systemParamOptions = systemParamOptions;
        this.repoApplication = repoApplication;
        this.repoROF = repoROF;
    }

    public async Task<bool> Handle(ReleaseApplicationPaymentsCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var releaseOfFunds = mapper.Map<IEnumerable<FloodParcelReleaseOfFundsViewModel>, IEnumerable<FloodPropReleaseOfFundsEntity>>(request.Payments);
        bool requestROF;

        if(releaseOfFunds.Count() > 0)
        {
            foreach (var payment in releaseOfFunds)
            {
                requestROF = await repoROF.ReleaseApplicationPayments(payment);
            }
        }
        

        return true;
    }
}
