namespace PresTrust.FloodMitigation.Application.Commands;

public class ReinitiateApplicationCommand : IRequest<Unit>
{
    public int ApplicationId { get; set; }
}
