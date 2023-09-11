namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelFinanceCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public decimal EstimatePurchasePrice { get; set; }
    public decimal AdditionalSoftCostEstimate { get; set; }
    public decimal AppraisedValue { get; set; }
    public decimal AMV { get; set; }
    public decimal TotalFEMABenifits { get; set; }
    public string? DOBAffidavitType { get; set; }
    public decimal DOBAffidavitAmt { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public decimal AppraisersFee { get; set; }
    public decimal SurveyorsFee { get; set; }
}
