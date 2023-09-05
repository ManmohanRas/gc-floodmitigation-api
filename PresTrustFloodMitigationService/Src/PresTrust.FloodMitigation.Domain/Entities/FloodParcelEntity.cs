namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelEntity
{
    public string PamsPin { get; set; }
    public int AgencyId { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QCode { get; set; }
    public string PropertyAddress { get; set; }
    public string LandOwner { get; set; }
    public string TargetArea { get; set; }
    public bool AlreadyExists { get; set; }
    public bool IsLocked { get; set; }
}
