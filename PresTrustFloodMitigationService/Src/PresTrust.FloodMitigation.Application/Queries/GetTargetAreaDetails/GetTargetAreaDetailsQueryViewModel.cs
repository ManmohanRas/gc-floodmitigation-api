namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTargetAreaDetailsQueryViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string TargetArea { get; set; }
    public DateTime? CreatedDate { get; set; }
    public IEnumerable<GetFloodFlapParcelViewModel>? Parcels { get; set; }
}

public class GetFloodFlapParcelViewModel: FloodParcelEntity
{
}
