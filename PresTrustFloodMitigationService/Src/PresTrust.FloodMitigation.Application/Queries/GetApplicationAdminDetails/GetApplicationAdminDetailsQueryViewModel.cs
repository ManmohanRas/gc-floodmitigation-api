namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationAdminDetailsQueryViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public DateTime MunicipalSupportDate { get; set; }
    public string MunicipalSupportNumber { get; set; }
    public DateTime FMCApprovalDate { get; set; }
    public string FMCPrelimNumber { get; set; }

}