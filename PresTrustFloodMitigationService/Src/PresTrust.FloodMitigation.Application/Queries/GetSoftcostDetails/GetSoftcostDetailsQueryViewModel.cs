namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftCostDetailsQueryViewModel
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public decimal CostShare { get; set; }
    public IEnumerable<FloodParcelSoftCostViewModel>? SoftCostLineItems { get; set; }
    public List<PropertyDocumentTypeViewModel> DocumentsTree { get; set; } = new List<PropertyDocumentTypeViewModel>();
}
