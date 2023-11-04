namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodFinanceLineItemEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string PropertyLocation { get; set; }
    public int Priority { get; set; }
    public decimal ValueEstimate { get; set; }
    public decimal FundsRequested { get; set; }
    public decimal MunicipalMatch { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}
