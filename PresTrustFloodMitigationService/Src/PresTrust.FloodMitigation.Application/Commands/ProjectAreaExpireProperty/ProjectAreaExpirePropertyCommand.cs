namespace PresTrust.FloodMitigation.Application.Commands;

public class ProjectAreaExpirePropertyCommand : IRequest<ProjectAreaExpirePropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string PamsPin { get; set; }
}
