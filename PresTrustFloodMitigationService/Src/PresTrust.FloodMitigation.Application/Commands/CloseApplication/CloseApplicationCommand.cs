namespace PresTrust.FloodMitigation.Application.Commands;

public class CloseApplicationCommand : IRequest<CloseApplicationCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
