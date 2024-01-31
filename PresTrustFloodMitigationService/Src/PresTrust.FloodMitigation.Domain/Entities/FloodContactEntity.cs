namespace PresTrust.FloodMitigation.Domain.Entities;
public class FloodContactEntity
{   
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? ContactName { get; set; }
    public string? Agency { get; set; }
    public string? Email { get; set; }
    public string? MainNumber { get; set; }
    public string? AlternateNumber { get; set; }
    public bool SelectContact { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
