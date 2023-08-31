namespace PresTrust.FloodMitigation.Application.Queries;

public class GetSoftcostDetailsQueryViewModel
{
    public int ApplicationId { get; set; }
    public IEnumerable<FloodParcelSoftcostViewModel>? SoftcostLineItems { get; set; }
}
