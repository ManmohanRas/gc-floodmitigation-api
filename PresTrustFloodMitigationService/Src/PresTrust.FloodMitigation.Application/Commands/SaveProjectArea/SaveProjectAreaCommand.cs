namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class SaveProjectAreaCommand : IRequest<SaveProjectAreaCommandViewModel>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public int ApplicationSubTypeId { get; set; }
    public int? NoOfHomes { get; set; }
    public int? NoOfContiguousHomes { get; set; }
    public List<SaveProjectAreaFloodParcel> Parcels { get; set; }
    public class SaveProjectAreaFloodParcel
    {
        public string PamsPin { get; set; }
        public string? PropertyAddress { get; set; }
        public string? TargetArea { get; set; }
        public string Block { get; set; }
        public string Lot { get; set; }
        public string? QCode { get; set; }
        public string LandOwner { get; set; }
    }
}

