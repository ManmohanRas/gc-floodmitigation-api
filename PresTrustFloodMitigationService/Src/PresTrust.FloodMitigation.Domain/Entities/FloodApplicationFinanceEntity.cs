namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodApplicationFinanceEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public decimal MatchPercent { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
