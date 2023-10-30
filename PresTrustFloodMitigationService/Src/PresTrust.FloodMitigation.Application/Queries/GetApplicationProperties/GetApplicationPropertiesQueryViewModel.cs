namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationPropertiesQueryViewModel
{
    public string PamsPin { get; set; }
    public bool IsLocked { get; set; }
    public bool AlreadyExists { get; set; }
    public string PropertyAddress { get; set; }
    public string TargetArea { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QCode { get; set; }
    public string LandOwner { get; set; }
    public int Priority { get; set; }

}
