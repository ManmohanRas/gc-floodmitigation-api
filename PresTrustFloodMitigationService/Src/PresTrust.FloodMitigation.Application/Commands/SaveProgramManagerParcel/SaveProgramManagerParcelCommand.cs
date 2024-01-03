namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveProgramManagerParcelCommand : IRequest<int>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public bool IsElevated { get; set; }
    public string? StreetNo { get; set; }
    public string? StreetAddress { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string? QCode { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public decimal? Acreage { get; set; }
    public int? YearOfConstruction { get; set; }
    public decimal? SquareFootage { get; set; }
    public string? LandOwner { get; set; }
    public string? OwnersAddress1 { get; set; }
    public string? OwnersAddress2 { get; set; }
    public string? OwnersCity { get; set; }
    public string? OwnersState { get; set; }
    public string? OwnersZipcode { get; set; }
    public decimal? TotalAssessedValue { get; set; }
    public decimal? LandValue { get; set; }
    public decimal? ImprovementValue { get; set; }
    public decimal? AnnualTaxes { get; set; }
}
