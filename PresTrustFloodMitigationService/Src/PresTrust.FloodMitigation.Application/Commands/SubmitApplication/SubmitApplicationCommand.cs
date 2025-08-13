namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApplicationCommand : IRequest<SubmitApplicationCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
