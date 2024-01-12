namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalFinanceQueryViewModel
{
    public int Id { get; set; } = 0;
    public int AgencyId { get; set; }
    public string YearOfInception { get; set; } = "";
    public bool AcquisitionOfLands { get; set; } = false;
    public bool AcquisitionOfFarmLands { get; set; } = false;
    public bool DevelopmentOfLands { get; set; } = false;
    public bool MaintenanceOfLands { get; set; } = false;
    public bool SalariesAndBenefits { get; set; } = false;
    public bool BondDownPayments { get; set; } = false;
    public bool HistoricPreservation { get; set; } = false;
    public bool OpenspaceMasterPlan { get; set; } = false;
    public DateTime? OpenspaceMasterPlanDate { get; set; } = DateTime.MinValue;
    public bool GreenAcresGrant { get; set; } = false;
    public string Other { get; set; } = "";
    public string TrustFundComments { get; set; } = "";
    public List<FloodMunicipalFinanceEntity>? MunicipalFinances { get; set; } = new List<FloodMunicipalFinanceEntity>();
}
