namespace PresTrust.FloodMitigation.Application.Commands;

public class RejectApplicationCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
}
