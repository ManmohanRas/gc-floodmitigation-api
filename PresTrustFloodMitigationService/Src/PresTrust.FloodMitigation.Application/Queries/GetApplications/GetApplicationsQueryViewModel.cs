namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsQueryViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
    public string Title { get; set; }
    public int ApplicationTypeId { get; set; }
    public string ApplicationType { get; set; }
    public int ApplicationSubTypeId { get; set; }
    public string ApplicationSubType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Status { get; set; }
    public bool CreatedByProgramAdmin { get; set; }
}
