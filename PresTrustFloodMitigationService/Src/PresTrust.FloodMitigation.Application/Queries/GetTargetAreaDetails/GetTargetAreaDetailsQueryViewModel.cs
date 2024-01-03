namespace PresTrust.FloodMitigation.Application.Queries;

public class GetTargetAreaDetailsQueryViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string TargetArea { get; set; }
    public DateTime? CreatedDate { get; set; }
    public IEnumerable<GetFloodFlapParcelViewModel>? Parcels { get; set; }
}

public class GetFloodFlapParcelViewModel
{
    public int Id { get; set; }
    public string PamsPin { get; set; }
    public string? PropertyAddress { get; set; }
    public int? TargetAreaId { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string? QCode { get; set; }
    public string LandOwner { get; set; }
    public string StreetNo { get; set; }
    public string StreetAddress { get; set; }
    public DateTime? DateOfFLAP { get; set; }
    public bool IsElevated { get; set; }

}
