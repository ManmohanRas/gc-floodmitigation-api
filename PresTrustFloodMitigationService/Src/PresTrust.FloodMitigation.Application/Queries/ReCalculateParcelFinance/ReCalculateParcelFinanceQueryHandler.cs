namespace PresTrust.FloodMitigation.Application.Queries;

public class ReCalculateParcelFinanceQueryHandler : IRequestHandler<ReCalculateParcelFinanceQuery, ReCalculateParcelFinanceQueryViewModel>
{
    private readonly IMapper mapper;
    private IParcelFinanceRepository repoParcelFinance;

    public ReCalculateParcelFinanceQueryHandler(
        IMapper mapper
       ,IParcelFinanceRepository repoParcelFinance
        )
    {
        this.mapper = mapper;
        this.repoParcelFinance = repoParcelFinance;
    }
    public Task<ReCalculateParcelFinanceQueryViewModel> Handle(ReCalculateParcelFinanceQuery request, CancellationToken cancellationToken)
    {
        var parcelFinance = mapper.Map<ReCalculateParcelFinanceQuery, ReCalculateParcelFinanceQueryViewModel>(request);

        //recalculate
        parcelFinance.HouseEncubrance = request.EstimatePurchasePrice * request.MatchPercent / 100;
        parcelFinance.SoftEstimateInit = (parcelFinance.HouseEncubrance) * 25 / 100;
        parcelFinance.SoftEstimate = parcelFinance.SoftEstimateInit + request.AdditionalSoftCostEstimate;
        parcelFinance.TotalEncumbresedFunds = parcelFinance.HouseEncubrance + parcelFinance.SoftEstimate;
        parcelFinance.DOBAmount = request.TotalFEMABenifits - request.DOBAffidavitAmt;
        parcelFinance.FinalOffer = request.AMV - parcelFinance.DOBAmount;
        parcelFinance.HardCostFMPAmt = request.AMV - parcelFinance.DOBAmount * request.MatchPercent / 100;

        return Task.FromResult(parcelFinance);
    }
}
