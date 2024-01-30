namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodAnnualFundingEntity
{
    public int Id { get; set; }
    public string AllocationYear { get; set; }
    public decimal AllocationAmount { get; set; }
    public decimal Interest { get; set; }
    public decimal AddedOrOmittedAmount { get; set; }
    public string? Comment { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
