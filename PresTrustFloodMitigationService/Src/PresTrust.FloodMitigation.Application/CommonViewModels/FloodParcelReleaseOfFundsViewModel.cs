namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodParcelReleaseOfFundsViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public string? Property { get; set; } = "";
    public string? CAFNumber { get; set; }
    public DateTime HardCostPaymentDate { get; set; }
    public DateTime SoftCostPaymentDate { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public string? HardCostPaymentStatus { get; set; }
    public string? SoftCostPaymentStatus { get; set; }
    public string? HardCostPaymentType { get; set; }
    public string? SoftCostPaymentType { get; set; }
    public int HardCostPaymentStatusId { get; set; }
    public int SoftCostPaymentStatusId { get; set; }

}
