namespace PresTrust.FloodMitigation.Application.Queries;

public class ReadTargetListFileQueryViewModel
{

}

public class ReadTargerListParcels
{
    public string PamsPin { get; set; }
    public string? AgencyId { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string StreetNo { get; set; }
    public string StreetAddress { get; set; }
    public string LandOwner { get; set; }
    public string TargetArea { get; set; }
    public bool IsFLAP { get; set; }
    public DateTime DateOfFLAP { get; set; }
}
