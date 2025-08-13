namespace PresTrust.FloodMitigation.Application.Commands;

public class ProjectAreaExpirePropertyCommand : IRequest<ProjectAreaExpirePropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }
}
