namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationParcelStatusLogEntity
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int StatusId { get; set; }
    public DateTime StatusDate { get; set; }
    public string Notes { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}


