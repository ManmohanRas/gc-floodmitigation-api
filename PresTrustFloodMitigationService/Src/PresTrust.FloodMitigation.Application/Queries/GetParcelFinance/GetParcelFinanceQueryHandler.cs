using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelFinanceQueryHandler : BaseHandler, IRequestHandler<GetParcelFinanceQuery, GetParcelFinanceQueryViewModel>
{
    private IMapper mapper;
    private IApplicationRepository repoApplication;
    private IParcelFinanceRepository repoParcelFinance;
    private ISoftCostRepository repoSoftCost;
    private readonly IApplicationParcelRepository repoAppParcel;

    public GetParcelFinanceQueryHandler(
        IMapper mapper
       ,IApplicationRepository repoApplication
       ,IParcelFinanceRepository repoParcelFinance
       ,ISoftCostRepository repoSoftCost,
        IApplicationParcelRepository repoAppParcel
        ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoParcelFinance = repoParcelFinance;
        this.repoSoftCost = repoSoftCost;
        this.repoAppParcel = repoAppParcel;
    }
    public async Task<GetParcelFinanceQueryViewModel> Handle(GetParcelFinanceQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // get parcel finance
        var parcelFinance = await this.repoParcelFinance.GetParceFinanceAsync(request.ApplicationId, request.PamsPin);
        var softCosts = await this.repoSoftCost.GetAllSoftCostLineItemsAsync(request.ApplicationId, request.PamsPin);

        var result = mapper.Map<FloodParcelFinanceEntity, GetParcelFinanceQueryViewModel>(parcelFinance);

        //softCosts totals
        if ((bool)property.IsApproved)
        {
            result.MunicipalAppraisersFee = softCosts.Where(x => x.SoftCostTypeId == (int)SoftCostTypeEnum.APPRAISALS).Sum(x => x.PaymentAmount);
            result.EnvAnalysis = softCosts.Where(x => x.SoftCostTypeId == (int)SoftCostTypeEnum.ENVIRONMENTAL_ANALYSIS).Sum(x => x.PaymentAmount);
            result.TitleSrchIns = softCosts.Where(x => x.SoftCostTypeId == (int)SoftCostTypeEnum.TITLE_SEARCH_INSURANCE).Sum(x => x.PaymentAmount);
            result.MunicipalSurveyorsFee = softCosts.Where(x => x.SoftCostTypeId == (int)SoftCostTypeEnum.SURVEY).Sum(x => x.PaymentAmount);
            result.DemolitionFee = softCosts.Where(x => x.SoftCostTypeId == (int)SoftCostTypeEnum.DEMOLITION).Sum(x => x.PaymentAmount);
            result.OtherSoftCost = softCosts.Where(x => x.SoftCostTypeId == (int)SoftCostTypeEnum.OTHER_SOFT_COSTS).Sum(x => x.PaymentAmount);
            result.TotalSoftCost = result.MunicipalAppraisersFee + result.EnvAnalysis + result.TitleSrchIns + result.MunicipalSurveyorsFee + result.DemolitionFee + result.OtherSoftCost;
        }
        else
        {
            result.SoftCostFMPAmt = default;
        }
        
        result.ReimbursedHardandSoftCosts = parcelFinance.ReimbursedHardCost + parcelFinance.ReimbursedSoftCost;

        return result;
    }
}
