using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleasePaymentsCommandHandler: BaseHandler, IRequestHandler<ReleasePaymentsCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IPropReleaseOfFundsRepository repoROF;


    public ReleasePaymentsCommandHandler(
        IMapper mapper, IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IPropReleaseOfFundsRepository repoROF
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoROF = repoROF;
    }

    public async Task<bool> Handle(ReleasePaymentsCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var releaseOfFunds = mapper.Map<IEnumerable<FloodParcelReleaseOfFundsViewModel>, IEnumerable<FloodPropReleaseOfFundsEntity>>(request.Payments);
        
        bool requestROF;

        if(releaseOfFunds.Count() > 0)
        {
            foreach (var payment in releaseOfFunds)
            {
                    payment.HardCostPaymentStatusId = request.Payments.Select(x => x.HardCostPaymentStatusId).FirstOrDefault();
                    payment.SoftCostPaymentStatusId = request.Payments.Select(x => x.SoftCostPaymentStatusId).FirstOrDefault();
                    
                    requestROF = await repoROF.ReleasePayments(payment);
            }
        }
        

        return true;
    }
}
