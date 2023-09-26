namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationSignatoryQueryViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string Designation { get; set; } = "";
    public string Title { get; set; } = "";
    public DateTime? SignedOn { get; set; }
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; }
}
