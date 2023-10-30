namespace PresTrust.FloodMitigation.Application.CommonViewModels;

public class FloodFinanceLineItemViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public string? PropertyLocation { get; set; }
    public int Priority { get; set; }
    public decimal ValueEstimate { get; set; }
    public decimal FundsRequested { get; set; }
    public decimal MunicipalMatch { get; set; }
}
