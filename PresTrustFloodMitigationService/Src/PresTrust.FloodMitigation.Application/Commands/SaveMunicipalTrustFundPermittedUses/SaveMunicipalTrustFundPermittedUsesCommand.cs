namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalTrustFundPermittedUsesCommand:IRequest<int>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string UserId { get; set; }
    public string YearOfInception { get; set; }
    public bool AcquisitionOfLands { get; set; }
    public bool AcquisitionOfFarmLands { get; set; }
    public bool DevelopmentOfLands { get; set; }
    public bool MaintenanceOfLands { get; set; }
    public bool SalariesAndBenefits { get; set; }
    public bool BondDownPayments { get; set; }
    public bool HistoricPreservation { get; set; }
    public bool OpenspaceMasterPlan { get; set; }
    public DateTime? OpenspaceMasterPlanDate { get; set; }
    public bool GreenAcresGrant { get; set; }
    public string? Other { get; set; }
    public string? TrustFundComments { get; set; }
}
