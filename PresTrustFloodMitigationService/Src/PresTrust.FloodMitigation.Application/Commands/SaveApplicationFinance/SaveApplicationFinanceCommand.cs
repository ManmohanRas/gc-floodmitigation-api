namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class represents api's query input model and returns the response object
/// </summary>
public class SaveApplicationFinanceCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public decimal MatchPercent { get; set; }
    public IEnumerable<FloodFundingSourceViewModel>? FundingSources { get; set; }
    public IEnumerable<FloodFinanceLineItemViewModel>? FinanceLineItems { get; set; }

}
