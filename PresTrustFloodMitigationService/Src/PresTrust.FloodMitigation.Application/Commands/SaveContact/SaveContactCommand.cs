namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveContactCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string ContactName { get; set; }
    public string Agency { get; set; }
    public string Email { get; set; }
    public string MainNumber { get; set; }
    public string AlternateNumber { get; set; }
    public bool SelContact { get; set; }
}
