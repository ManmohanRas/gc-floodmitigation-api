namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodFlapCommentEntity
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string Comment { get; set; }
    public string? RowStatus { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
