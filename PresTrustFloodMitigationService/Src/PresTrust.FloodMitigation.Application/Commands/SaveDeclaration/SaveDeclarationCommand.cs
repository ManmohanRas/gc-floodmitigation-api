namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDeclarationCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
    public string Title { get; set; }
    public int AgencyId { get; set; }
    public string ApplicationType { get; set; }
    public string ApplicationSubType { get; set; }
    public List<SaveDeclarationFloodParcel> Parcels { get; set; }
    public List<FloodApplicationUserViewModel> ApplicationUsers { get; set; }

    public class SaveDeclarationFloodParcel
    {
        public int Id { get; set; }
        public string PamsPin { get; set; }
        public string? PropertyAddress { get; set; }
        public string? TargetArea { get; set; }
        public string Block { get; set; }
        public string Lot { get; set; }
        public string? QCode { get; set; }
        public string? LandOwner { get; set; }
        public bool IsValidPamsPin { get; set; }
    }

}
