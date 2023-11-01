namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationReleaseOfFundsQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int CAFNumber { get; set; }
    public bool CAFClosed { get; set; }
    public decimal? CAFAmount { get; set; }
    public decimal? AmountSpent { get; set; }
    public decimal? Balance { get; set; }
    public IEnumerable<FloodParcelReleaseOfFundsViewModel>? Payments { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
}
