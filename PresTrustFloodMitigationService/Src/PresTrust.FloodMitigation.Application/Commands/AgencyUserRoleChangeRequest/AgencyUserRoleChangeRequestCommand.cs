namespace PresTrust.FloodMitigation.Application.Commands
{
    public class AgencyUserRoleChangeRequestCommand : IRequest<bool>
    {
        public int AgencyId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string NewRole { get; set; }
    }
}
