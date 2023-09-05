namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftcostCommandViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string PamsPin { get; set; } = "";
    public int SoftcostTypeId { get; set; } = 0;
    public string VendorName { get; set; } = "";
    public decimal InvoiceAmount { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal CostShare { get; set; }
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; } = DateTime.MinValue;
}
