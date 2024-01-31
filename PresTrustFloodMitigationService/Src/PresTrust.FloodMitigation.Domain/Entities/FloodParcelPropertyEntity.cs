namespace PresTrust.FloodMitigation.Domain.Entities;

public class FloodParcelPropertyEntity
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
    public int Priority { get; set; }
    public decimal ValueEstimate { get; set; }
    public decimal EstimatedPurchasePrice { get; set; }
    public decimal BRV { get; set; }
    public string? NfipPolicyNo { get; set; }
    public string? SourceOfValueEstimate { get; set; }
    public decimal FirstFloorElevation { get; set; }
    public int StructureType { get; set; }
    public int FoundationType { get; set; }
    public int OccupancyClass { get; set; }
    public int PercentageOfDamage { get; set; }
    public bool HasContaminants { get; set; }
    public bool IsLowIncomeHousing { get; set; }
    public bool HasHistoricSignificance { get; set; }
    public bool IsRentalProperty { get; set; }
    public decimal RentPerMonth { get; set; }
    public bool NeedSoftCost { get; set; }
    public bool IsPreIrenePropertyOwner { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
}