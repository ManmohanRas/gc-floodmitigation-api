namespace PresTrust.FloodMitigation.Application.Queries;

public class GetAnnualFundingDetailsQueryViewModel
{
    public int Id { get; set; }
    public string AllocationYear { get; set; }
    public decimal AllocationAmount { get; set; }
    public decimal Interest { get; set; }
    public decimal AddedOrOmittedAmount { get; set; }
    public string Comment { get; set; }
}
