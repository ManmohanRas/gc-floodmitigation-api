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
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public int AgencyId { get; set; }
    public string? AgencyName { get; set; }
    public string Block { get; set; }
    public string Lot { get; set; }
    public string QCode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string StreetNo { get; set; }
    public string StreetAddress { get; set; }
    public string PropertyAddress { get; set; }
    public decimal Acreage { get; set; }
    public string LandOwner { get; set; }
    public string OwnersAddress1 { get; set; }
    public string OwnersAddress2 { get; set; }
    public string OwnersCity { get; set; }
    public string OwnersState { get; set; }
    public string OwnersZipcode { get; set; }
    public decimal SquareFootage { get; set; }
    public int YearOfConstruction { get; set; }
    public decimal TotalAssessedValue { get; set; }
    public decimal LandValue { get; set; }
    public decimal ImprovementValue { get; set; }
    public decimal AnnualTaxes { get; set; }
    public string TargetArea { get; set; }
    public bool IsFLAP { get; set; }
    public DateTime? DateOfFLAP { get; set; }
    public bool IsElevated { get; set; }
    public bool IsValidPamsPin { get; set; }
    public string Status { get; set; }
    public string PrevStatus { get; set; }
    public bool IsLocked { get; set; }
    public bool? IsSubmitted { get; set; }
    public bool? IsApproved { get; set; }
    public bool AlreadyExists { get; set; }
}
