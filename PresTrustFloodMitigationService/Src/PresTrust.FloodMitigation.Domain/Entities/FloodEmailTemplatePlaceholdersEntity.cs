namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodEmailTemplatePlaceholdersEntity
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public string? PropertyAddress { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public DateTime? BccFinalApprovalDate { get; set; }
    public DateTime? CurrentExpirationDate { get; set; }

    public DateTime? GrantExpirationDate { get; set; }

    public DateTime? FundingExpirationDate { get; set; }
    public DateTime? FirstFundingExpirationDate { get; set; }
    public DateTime? SecondFundingExpirationDate { get; set; }

    public Decimal?  CAFAmount {get; set;}
}
