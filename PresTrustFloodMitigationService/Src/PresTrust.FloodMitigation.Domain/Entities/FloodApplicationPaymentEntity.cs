namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationPaymentEntity
{
    public int Id { get; set; } 
    public int ApplicationId { get; set; }
    public int CAFNumber { get; set; }
    public bool CAFClosed { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
}
