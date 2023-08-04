namespace PresTrust.FloodMitigation.Application.Commands;

public class RejectApplicationCommand : IRequest<Unit>
{
    public int ApplicationId { get; set; }
}
