namespace PresTrust.FloodMitigation.Application.Commands;

public class PendingPropertyCommand : IRequest<PendingPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string Pamspin { get; set; }
}
