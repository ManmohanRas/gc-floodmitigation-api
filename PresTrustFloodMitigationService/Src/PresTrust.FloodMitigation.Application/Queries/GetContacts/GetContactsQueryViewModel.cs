namespace PresTrust.FloodMitigation.Application.Queries;

public class GetContactsQueryViewModel
{   
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string ContactName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Agency { get; set; } = "";
    public int MainNumber { get; set; } = 0;
    public int AlternateNumber { get; set; } = 0;
    public bool SelectContact { get; set; } = false;
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; } = DateTime.MinValue;
}
