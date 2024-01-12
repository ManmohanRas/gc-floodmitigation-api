namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodMunicipalCommentEntity
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string? Comment { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }

}
