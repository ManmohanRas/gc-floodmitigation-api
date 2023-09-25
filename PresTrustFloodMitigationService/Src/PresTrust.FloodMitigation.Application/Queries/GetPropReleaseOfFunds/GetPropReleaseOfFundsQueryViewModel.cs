namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropReleaseOfFundsQueryViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public int Pamspin { get; set; } = 0;
    public string ProjectAreaName { get; set; } = "";
    public string Property { get; set; } = "";
    public string ReimburesedHradCost { get; set; } = "";
    public string ReimburesedSoftCost { get; set; } = "";
    public string ReimburesedHradSoftCost { get; set; } = "";
    public string CAFNumber { get; set; } = "";
    public string FinalCost { get; set; } = "";
    public string PaymentMode { get; set; } = "";
    public string BalanceAmount { get; set; } = "";
    public string ReimbureseType { get; set; } = "";
    public string ReimbureseAmount { get; set; } = "";
    public string PaymentType { get; set; } = "";
    public DateTime? DateTransfareNeeded { get; set; }
    public string PaymentStatus { get; set; } = "";
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; }
}
