namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropReleaseOfFundsCommand : IRequest<int> 
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public DateTime? HardCostPaymentDate { get; set; }
    public DateTime? SoftCostPaymentDate { get; set; }
    public string HardCostPaymentStatus { get; set; }
    public string SoftCostPaymentStatus { get; set; }
    public string HardCostPaymentType { get; set; }
    public string SoftCostPaymentType { get; set; }
}
