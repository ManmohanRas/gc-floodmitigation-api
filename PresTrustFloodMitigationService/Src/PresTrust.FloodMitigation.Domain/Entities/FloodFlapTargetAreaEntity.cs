namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodFlapTargetAreaEntity
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string TargetArea { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
