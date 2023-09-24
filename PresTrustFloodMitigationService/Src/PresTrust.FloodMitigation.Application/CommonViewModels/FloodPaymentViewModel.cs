namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodPaymentViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public int HardCostPaymentTypeId { get; set; }
    public DateTime HardCostPaymentDate { get; set; }
    public bool HardCostPaymentStatus { get; set; }
    public int SoftCostPaymentTypeId { get; set; }
    public DateTime SoftCostPaymentDate { get; set; }
    public bool SoftCostPaymentStatus { get; set; }

}
