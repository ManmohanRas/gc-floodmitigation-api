namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApplicationCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
}
