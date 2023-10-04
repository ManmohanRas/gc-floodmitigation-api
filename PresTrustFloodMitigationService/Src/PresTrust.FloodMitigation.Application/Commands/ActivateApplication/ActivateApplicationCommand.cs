namespace PresTrust.FloodMitigation.Application.Commands;

public class ActivateApplicationCommand : IRequest<ActivateApplicationCommandViewModel>
{
    public int ApplicationId { get; set; }
}
