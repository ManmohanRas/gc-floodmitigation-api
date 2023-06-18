namespace PresTrust.FloodMitigation.Application.Services.IdentityApi;

public class PutAgencyUserRoleChangeRequest
{
    public string Email { get; set; }
    public IdentityUserClaim Claim { get; set; }
    public IdentityUserClaim NewClaim { get; set; }
}
