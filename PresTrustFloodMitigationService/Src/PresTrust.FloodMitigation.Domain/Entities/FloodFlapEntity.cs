namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodFlapEntity
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public bool FlapApproved { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? LastRevisedDate { get; set; }
    public DateTime? FlapMailToGrantee { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
