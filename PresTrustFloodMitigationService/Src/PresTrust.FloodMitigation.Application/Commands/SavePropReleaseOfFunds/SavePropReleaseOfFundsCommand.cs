namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropReleaseOfFundsCommand : IRequest<int> 
{
    public int Id { get; set; }
    public string? ApplicationId { get; set; }
    public string? Pamspin { get; set; }
}
