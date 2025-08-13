namespace PresTrust.FloodMitigation.Application.Commands;

public class ReinitiatePropertyCommand : IRequest<Unit>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }
}
