namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapTargetAreaCommand: IRequest<int>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string TargetArea { get; set; }
    public DateTime? CreatedDate { get; set; }
    public List<SaveFloodFlapParcelViewModel>? Parcels { get; set; }
}

public class SaveFloodFlapParcelViewModel
{
    public int Id { get; set; }
    public string PamsPin { get; set; }
    public string? PropertyAddress { get; set; }
    public int? TargetAreaId { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string? QCode { get; set; }
    public string LandOwner { get; set; }
    public bool IsValidPamsPin { get; set; }
}
