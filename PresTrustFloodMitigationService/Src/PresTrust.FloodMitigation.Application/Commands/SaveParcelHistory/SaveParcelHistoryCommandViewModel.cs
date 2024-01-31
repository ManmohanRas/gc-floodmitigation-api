namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveParcelHistoryCommandViewModel
{
    public int Id { get; set; }
    public int ParcelId { get; set; }
    public string CurrentPamsPin { get; set; }
    public string PreviousPamsPin { get; set; }
    public string? Section { get; set; }
    public decimal? Acres { get; set; }
    public decimal? AcresToBeAcquired { get; set; }
    public bool? Partial { get; set; }
    public string? InterestType { get; set; }
    public bool? IsThisAnExclusionArea { get; set; }
    public string? Notes { get; set; }
    public string? EasementId { get; set; }
    public bool IsActive { get; set; }
    public string? ChangeType { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? ReasonForChange { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
