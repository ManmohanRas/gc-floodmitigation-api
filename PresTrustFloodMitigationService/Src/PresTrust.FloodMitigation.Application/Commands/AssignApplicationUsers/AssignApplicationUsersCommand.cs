namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class AssignApplicationUsersCommand: IRequest<Unit>
{
    public int ApplicationId { get; set; }
    public IEnumerable<FloodApplicationUserViewModel>? ApplicationUsers { get; set; }
}
