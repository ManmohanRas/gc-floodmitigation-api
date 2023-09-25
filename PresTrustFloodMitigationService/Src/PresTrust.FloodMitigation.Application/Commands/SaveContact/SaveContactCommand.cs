namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? ContactName { get; set; }
    public string? Agency { get; set; }
    public string? Email { get; set; }
    public int MainNumber { get; set; }
    public int AlternateNumber { get; set; }
    public bool SelectContact { get; set; }
}
