namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodEmailTemplatePlaceholderViewModel
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public string? PropertyAddress { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public DateTime? BccFinalApprovalDate { get; set; }
    public DateTime? CurrentExpirationDate {  get; set; }

    public DateTime? GrantExpirationDate { get; set; }

}
