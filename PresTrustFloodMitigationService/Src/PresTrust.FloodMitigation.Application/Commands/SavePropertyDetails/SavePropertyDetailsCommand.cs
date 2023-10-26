namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyDetailsCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
}
