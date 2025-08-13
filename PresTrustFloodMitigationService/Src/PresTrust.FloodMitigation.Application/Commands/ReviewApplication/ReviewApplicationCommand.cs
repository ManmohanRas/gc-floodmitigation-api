namespace PresTrust.FloodMitigation.Application.Commands;

public class ReviewApplicationCommand : IRequest<ReviewApplicationCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
