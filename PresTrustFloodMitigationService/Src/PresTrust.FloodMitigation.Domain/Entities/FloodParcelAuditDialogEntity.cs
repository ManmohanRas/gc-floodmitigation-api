namespace PresTrust.FloodMitigation.Domain.Entities;
public class FloodParcelAuditDialogEntity
{
    public int Id { get; set; }
    public int  AgencyId { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QualificationCode { get; set; }
    public bool Partial { get; set; }
    public string Section { get; set; }
    public double Acres { get; set; }
    public double AcresToBeAcquired { get; set; }
    public bool IsThisAnExclusionArea { get; set; }
    public string Notes { get; set; }
    public string PamsPin { get; set; }
    public string InterestType { get; set; }
    public string EasementId { get; set; }
    public string ChangeType { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string ReasonForChange { get; set; }
    public bool IsActive { get; set; }
    public int PamsPinStatus { get; set; }
    public string LastUpdatedBy {  get; set; }
    public DateTime LastUpdatedOn { get; set;}

}
    

