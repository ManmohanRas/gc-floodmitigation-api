namespace PresTrust.FloodMitigation.Application.Commands;

public class CountyUserRoleChangeRequestCommand : IRequest<bool>
{
    public string Email { get; set; }
    public string Role { get; set; }
    public string NewRole { get; set; }

}
