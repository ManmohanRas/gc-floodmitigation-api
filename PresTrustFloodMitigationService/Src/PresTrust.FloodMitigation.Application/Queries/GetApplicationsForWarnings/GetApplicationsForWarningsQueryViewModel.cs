namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsForWarningsQueryViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ApplicationStatus { get; set; }
    public string PropertyStatus { get; set; }
    public DateTime? StatusChangeDate { get; set; }
}
