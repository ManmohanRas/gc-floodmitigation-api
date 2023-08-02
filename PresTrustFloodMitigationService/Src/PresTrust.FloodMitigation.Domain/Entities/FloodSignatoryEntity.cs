namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodSignatoryEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string Designation { get; set; }
    public string Title { get; set; }
    public DateTime? SignatureOn { get; set; }
    public string SignatureType { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
