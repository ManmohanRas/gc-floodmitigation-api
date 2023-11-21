namespace PresTrust.FloodMitigation.Domain.Entities;
public class FloodPropReleaseOfFundsEntity
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string PamsPin { get; set; } = "";
    public string Property { get; set; } = "";
    public string CAFNumber { get; set; } = "";
    public int HardCostPaymentTypeId { get; set; }
    public DateTime? HardCostPaymentDate { get; set; }
    public int HardCostPaymentStatusId { get; set; }
    public int SoftCostPaymentTypeId { get; set; }
    public DateTime? SoftCostPaymentDate { get; set; }
    public int SoftCostPaymentStatusId { get; set; }
    public decimal HardCostFMPAmt { get; set; }
    public decimal SoftCostFMPAmt { get; set; }
    public string LastUpdatedBy { get; set; } = "";

    public PaymentStatusEnum HardCostPaymentStatus
    {
        get
        {
            return (PaymentStatusEnum)HardCostPaymentStatusId;
        }
        set
        {
            this.HardCostPaymentStatusId = (int)value;
        }
    }
    public PaymentStatusEnum SoftCostPaymentStatus
    {
        get
        {
            return (PaymentStatusEnum)SoftCostPaymentStatusId;
        }
        set
        {
            this.SoftCostPaymentStatusId = (int)value;
        }
    }

    public PaymentTypeEnum HardCostPaymentType
    {
        get
        {
            return (PaymentTypeEnum)HardCostPaymentTypeId;
        }
        set
        {
            this.HardCostPaymentTypeId = (int)value;
        }
    }
    public PaymentTypeEnum SoftCostPaymentType
    {
        get
        {
            return (PaymentTypeEnum)SoftCostPaymentTypeId;
        }
        set
        {
            this.SoftCostPaymentTypeId = (int)value;
        }
    }
}
