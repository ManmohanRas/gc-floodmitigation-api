namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationFinanceDetailsQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public decimal MatchPercent { get; set; }
    public IEnumerable<FloodFundingSourceViewModel>? FundingSources { get; set; }
    public IEnumerable<FloodFinanceLineItemViewModel>? FinanceLineItems { get; set; }
}
