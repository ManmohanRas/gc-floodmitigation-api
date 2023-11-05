namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationReleaseOfFundsQueryHandler: BaseHandler, IRequestHandler<GetApplicationReleaseOfFundsQuery, GetApplicationReleaseOfFundsQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationReleaseOfFundsRepository repoApplicationROF;

    public GetApplicationReleaseOfFundsQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IApplicationReleaseOfFundsRepository repoApplicationROF
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoApplicationROF = repoApplicationROF;
    }

    public async Task<GetApplicationReleaseOfFundsQueryViewModel> Handle(GetApplicationReleaseOfFundsQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var releaseOfFunds = await repoApplicationROF.GetReleaseOfFundsAsync(application.Id);

        var payments = await repoApplicationROF.GetApplicationPaymentsAsync(application.Id);

        //Amounts released
        var amountSpent =  payments.Where(x => x.HardCostPaymentStatusId == 1 || x.SoftCostPaymentStatusId == 1).Sum(y => y.SoftCostFMPAmt  + y.HardCostFMPAmt);
        
        //mapping data
        var result = mapper.Map<FloodApplicationReleaseOfFundsEntity, GetApplicationReleaseOfFundsQueryViewModel>(releaseOfFunds);
        result.CAFAmount = 0;
        result.AmountSpent = amountSpent;
        result.Balance = amountSpent;
        result.Payments = mapper.Map<IEnumerable<FloodPropReleaseOfFundsEntity>, IEnumerable<FloodParcelReleaseOfFundsViewModel>>(payments);

        return result;
    }
}
