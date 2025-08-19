namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveParcelPropertyCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int AgencyId { get; set; }
    public string UserId { get; set; }

    //Flood Parcel Property
    public int Priority { get; set; }
    public decimal? ValueEstimate { get; set; }
    public decimal? EstimatedPurchasePrice { get; set; }
    public decimal? BRV { get; set; }
    public string? NfipPolicyNo { get; set; }
    public string? SourceOfValueEstimate { get; set; }
    public decimal? FirstFloorElevation { get; set; }
    public int? StructureType { get; set; }
    public int? FoundationType { get; set; }
    public int? OccupancyClass { get; set; }
    public decimal? PercentageOfDamage { get; set; }
    public bool? HasContaminants { get; set; }
    public bool? IsLowIncomeHousing { get; set; }
    public bool? HasHistoricSignificance { get; set; }
    public bool? IsRentalProperty { get; set; }
    public decimal? RentPerMonth { get; set; }
    public bool? NeedSoftCost { get; set; }
    public bool? IsPreIrenePropertyOwner { get; set; }

    //Flood Parcel
    public string Block { get; set; }
    public string Lot { get; set; }
    public string? QCode { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? StreetNo { get; set; }
    public string? StreetAddress { get; set; }
    public decimal? Acreage { get; set; }
    public string? LandOwner { get; set; }
    public string? OwnersAddress1 { get; set; }
    public string? OwnersAddress2 { get; set; }
    public string? OwnersCity { get; set; }
    public string? OwnersState { get; set; }
    public string? OwnersZipcode { get; set; }
    public decimal? SquareFootage { get; set; }
    public int? YearOfConstruction { get; set; }
    public decimal? TotalAssessedValue { get; set; }
    public decimal? LandValue { get; set; }
    public decimal? ImprovementValue { get; set; }
    public decimal? AnnualTaxes { get; set; }
}
