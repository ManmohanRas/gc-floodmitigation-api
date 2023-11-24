namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftCostCommand : IRequest<Unit>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public IEnumerable<SaveSoftCostModel>? SoftCostLineItems { get; set; }
    
}

public class SaveSoftCostModel
{
    public int Id { get; set; } = 0;
    public int SoftCostTypeId { get; set; } = 0;
    public string VendorName { get; set; } = "";
    public decimal InvoiceAmount { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal CostShare { get; set; }
    public bool IsSubmitted { get; set; }
    public bool IsApproved { get; set; }
}
