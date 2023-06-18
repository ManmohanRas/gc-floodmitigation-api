namespace PresTrust.FloodMitigation.Application.Services.IdentityApi;

public class PostUserRoleRequest
{
    public string Email { get; set; }
    public string[] Roles { get; set; }
    public IdentityUserClaim[] Claims { get; set; }
}
