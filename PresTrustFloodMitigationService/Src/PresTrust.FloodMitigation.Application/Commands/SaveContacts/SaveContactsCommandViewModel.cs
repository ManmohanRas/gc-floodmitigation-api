namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactsCommandViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string? ContactName { get; set; }
    public string? Agency { get; set; }
    public string? Email { get; set; }
    public string? MainNumber { get; set; }
    public string? AlternateNumber { get; set; }
    public bool SelectContact { get; set; }
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; } = DateTime.MinValue;
}
