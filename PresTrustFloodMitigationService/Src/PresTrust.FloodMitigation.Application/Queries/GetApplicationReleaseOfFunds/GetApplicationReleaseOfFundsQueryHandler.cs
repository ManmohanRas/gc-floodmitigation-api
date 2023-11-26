using System.Linq;

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

        

        decimal amountSpent = 0;
        decimal houseEncubrance = 0;
        decimal softEstimate = 0;
        //Amounts released
        if (payments.Count() > 0)
        {
            amountSpent = payments.Where(x => x.SoftCostPaymentStatusId == 1).Sum(y => y.SoftCostFMPAmt);
            amountSpent += payments.Where(x => x.HardCostPaymentStatusId == 1).Sum(y => y.HardCostFMPAmt);

            var estimatePurchasePrice = payments.Sum(y => y.EstimatePurchasePrice);
            houseEncubrance = payments.Sum(y => y.EstimatePurchasePrice * y.MatchPercent / 100) ?? 0;
            var softEstimateInit = houseEncubrance * 25 / 100;
            var additionalSoftCostEstimate = payments.Sum(y => y.AdditionalSoftCostEstimate);
            softEstimate = softEstimateInit + additionalSoftCostEstimate ?? 0;
        }

        //mapping data
        var result = mapper.Map<FloodApplicationReleaseOfFundsEntity, GetApplicationReleaseOfFundsQueryViewModel>(releaseOfFunds);
        result.CAFAmount = houseEncubrance + softEstimate;
        result.AmountSpent = amountSpent;
        result.Balance = (result.CAFAmount) - (result.AmountSpent);
        result.Payments = mapper.Map<IEnumerable<FloodPropReleaseOfFundsEntity>, IEnumerable<FloodParcelReleaseOfFundsViewModel>>(payments);

        return result;
    }
}
