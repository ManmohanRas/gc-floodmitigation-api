namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationOverviewQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int? NoOfHomes { get; set; }
    public int? NoOfContiguousHomes { get; set; }
    public bool NatlDisaster { get; set; }
    public int NatlDisasterId { get; set; }
    public string NatlDisasterName { get; set; }
    public int NatlDisasterYear { get; set; }
    public int NatlDisasterMonth { get; set; }
    public bool LOI { get; set; }
    public string LOIStatus { get; set; }
    public DateTime? LOIApprovedDate { get; set; }
    public bool FEMA_OR_NJDEP_Applied { get; set; }
    public bool FEMAApplied { get; set; }
    public string FEMAStatus { get; set; }
    public DateTime? FEMAApprovedDate { get; set; }
    public string FEMADenialReason { get; set; }
    public bool GreenAcresApplied { get; set; }
    public string GreenAcresStatus { get; set; }
    public DateTime? GreenAcresApprovedDate { get; set; }
    public bool BlueAcresApplied { get; set; }
    public string BlueAcresStatus { get; set; }
    public DateTime? BlueAcresApprovedDate { get; set; }
    public bool FundingAgenciesApplied { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
    public IEnumerable<FloodApplicationFundingAgencyViewModel>? FundingAgencies { get; set; }

}
