using DevExpress.CodeParser;

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
        parcelFinance.HouseEncubrance = request.EstimatePurchasePrice * request.MatchPercent;
        parcelFinance.SoftEstimate = (parcelFinance.HouseEncubrance) * 25 / 100;
        parcelFinance.SoftEstimateTotal = parcelFinance.SoftEstimate + request.AdditionalSoftCostEstimate;
        parcelFinance.TotalEncumbresedFunds = (request.EstimatePurchasePrice) * (request.MatchPercent) + parcelFinance.SoftEstimate;
        parcelFinance.DOBAmount = request.TotalFEMABenifits - request.HomeOwnerDOBAffidavit;
        parcelFinance.FinalOffer = request.AMV - parcelFinance.DOBAmount;
        parcelFinance.HardCostFMPAmtReimbursed = request.AMV - parcelFinance.DOBAmount * (request.MatchPercent);

        return Task.FromResult(parcelFinance);
    }
}
