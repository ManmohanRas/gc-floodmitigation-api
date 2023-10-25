namespace PresTrust.FloodMitigation.Application.Commands;

public class RejectPropertyCommand : IRequest<RejectPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string Pamspin { get; set; }
}
