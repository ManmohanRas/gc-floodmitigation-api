namespace PresTrust.FloodMitigation.Application.Queries;
/// <summary>
/// This class represents api's query input model and returns the response object
/// </summary>
public class GetFloodParcelsByFilterQuery : IRequest<IEnumerable<GetFloodParcelsByFilterQueryViewModel>>
{
    public int AgencyId { get; set; }
    public string? Block { get; set; }
    public string? Lot { get; set; }
    public string? Address { get; set; }
    public List<string>? ExistingPamsPins { get; set; }
    public bool IsDOI { get; set; } = false;
}
