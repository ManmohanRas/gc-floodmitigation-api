namespace PresTrust.FloodMitigation.Application.Services.IdentityApi
{
    public class PutCountyUserRoleChangeRequest
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string NewRole { get; set; }
    }
}
