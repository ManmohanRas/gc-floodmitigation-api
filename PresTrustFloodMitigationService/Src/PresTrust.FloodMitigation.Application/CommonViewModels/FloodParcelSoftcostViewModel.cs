namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodParcelSoftCostViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; } 
    public int SoftCostTypeId { get; set; } 
    public string? VendorName { get; set; }
    public decimal InvoiceAmount { get; set; }
    public decimal PaymentAmount { get; set; }
    public bool IsSubmitted { get; set; }
    public bool IsApproved { get; set; }
}
