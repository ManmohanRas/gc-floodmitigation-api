namespace PresTrust.FloodMitigation.Application.Queries;

public class GetParcelFinanceQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public decimal ValueEstimate { get; set; }
    public decimal EstimatePurchasePrice { get; set; }
    public decimal MatchPercent { get; set; }
    public decimal HouseEncubrance { get; set; }
    public decimal SoftEstimateInit { get; set; }
    public decimal AdditionalSoftCostEstimate { get; set; }
    public decimal SoftEstimate { get; set; }
    public decimal TotalEncumbresedFunds { get; set; }
    public decimal AppraisedValue { get; set; }
    public decimal AMV { get; set; }
    public decimal TotalFEMABenifits { get; set; }
    public string? DOBAffidavitType { get; set; }
    public decimal DOBAffidavitAmt { get; set; }
    public decimal DOBAmount { get; set; }
    public decimal FinalOffer { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public DateTime? HardCostReimbursedDate { get; set; }
    public decimal MunicipalAppraisersFee { get; set; }
    public decimal MunicipalSurveyorsFee { get; set; }
    public decimal TitleSrchIns { get; set; }
    public decimal EnvAnalysis { get; set; }
    public decimal DemolitionFee { get; set; }
    public decimal OtherSoftCost { get; set; }
    public decimal TotalSoftCost { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public DateTime? SoftCostReimbursedDate { get; set; }
    public decimal ReimbursedHardandSoftCosts { get; set; }
    public decimal NetParcelFunds { get; set; }
    public decimal AppraisersFee { get; set; }
    public decimal SurveyorsFee { get; set; }
}
