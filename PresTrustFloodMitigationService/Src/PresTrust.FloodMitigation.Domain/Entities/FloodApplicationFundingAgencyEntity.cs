namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationFundingAgencyEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? FundingAgencyName { get; set; } 
    public string? CurrentStatus { get; set; }
    public DateTime? DateOfApproval { get; set; }
}
