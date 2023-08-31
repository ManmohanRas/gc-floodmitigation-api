namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodParcelSoftcostViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public int SoftcostTypeId { get; set; }
    public string? VendorName { get; set; }
    public decimal InvoiceAmount { get; set; }
    public decimal PaymentAmount { get; set; }

}
