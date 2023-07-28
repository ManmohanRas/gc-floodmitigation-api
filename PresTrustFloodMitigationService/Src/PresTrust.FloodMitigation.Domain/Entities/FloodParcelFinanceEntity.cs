namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelFinanceEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public string? PropertyAddress { get; set; }
    public string? Priority { get; set; }
    public decimal ValueEstimate { get; set; }
    public decimal FundsRequested { get; set; }
    public decimal MunicipalMatch { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
