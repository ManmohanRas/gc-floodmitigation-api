namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFloodParcelsByFilterQueryViewModel
{
    public string PamsPin { get; set; }
    public string PropertyAddress { get; set; }
    public string TargetArea { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QCode { get; set; }
    public string LandOwner { get; set; }
}
