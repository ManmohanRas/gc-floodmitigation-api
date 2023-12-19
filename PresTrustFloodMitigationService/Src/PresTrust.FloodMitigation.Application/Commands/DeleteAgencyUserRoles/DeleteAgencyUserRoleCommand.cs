namespace PresTrust.FloodMitigation.Application.Commands
{
    public class DeleteAgencyUserRoleCommand : IRequest<bool>
    {
        public int AgencyId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
