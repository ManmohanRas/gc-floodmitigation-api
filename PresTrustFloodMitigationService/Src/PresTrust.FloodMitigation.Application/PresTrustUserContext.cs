namespace PresTrust.FloodMitigation.Application;

public class AgencyUserRole
{
    public int AgencyId { get; set; }
    public UserRoleEnum UserRole { get; set; }
}

public class UserProfileModel
{
    public string Email { get; set; }
    public string Name { get; set; }
    public UserRoleEnum Role { get; set; }
    public List<int> AgencyIds { get; set; }
}

public interface IPresTrustUserContext
{
    string Name { get; }
    string Email { get; }
    UserRoleEnum Role { get; }
    List<int> AgencyIds { get; }
    bool IsExternalUser { get; }
    string AccessToken { get; }
    void DeriveRole(int agencyId);
}

public sealed class PresTrustUserContext : IPresTrustUserContext
{
    private readonly IHttpContextAccessor accessor;
    private UserProfileModel userProfile;
    private string accessToken;
    private bool isExternalUser = false;
    private List<AgencyUserRole> agencyUserRoles;
    public PresTrustUserContext(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
        DeriveUserProfile();
    }

    public string Name => userProfile.Name ?? "";
    public string Email => userProfile.Email ?? "";
    public UserRoleEnum Role => userProfile.Role;
    public List<int> AgencyIds => userProfile.AgencyIds ?? new List<int>();
    public bool IsExternalUser => this.isExternalUser;
    public string AccessToken => this.accessToken ?? "";

    public void DeriveRole(int agencyId)
    {
        if (Role != UserRoleEnum.NONE)
            return;

        var agencyUserRole = agencyUserRoles.Where(aur => aur.AgencyId == agencyId).FirstOrDefault();
        if (agencyUserRole != null)
        {
            userProfile.Role = agencyUserRole.UserRole;
        }
    }

    private void DeriveUserProfile()
    {
        if (this.accessor == null)
            throw new ArgumentNullException(nameof(accessor));

        userProfile = new UserProfileModel();
        userProfile.Role = UserRoleEnum.NONE;

        this.accessToken = accessor.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
        //userProfile.Email = this.accessor.HttpContext.User.FindFirst(IdentityClaimTypes.EMAIL)?.Value;
        //userProfile.Name = this.accessor.HttpContext.User.FindFirst(IdentityClaimTypes.NAME)?.Value;

        userProfile.Name = this.accessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
        userProfile.Email = this.accessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        if (this.accessor.HttpContext.User.IsInRole(IdentityRoles.FLOOD_PROGRAM_ADMIN))
            userProfile.Role = UserRoleEnum.PROGRAM_ADMIN;

        if (this.accessor.HttpContext.User.IsInRole(IdentityRoles.FLOOD_PROGRAM_EDITOR))
            userProfile.Role = UserRoleEnum.PROGRAM_EDITOR;

        if (this.accessor.HttpContext.User.IsInRole(IdentityRoles.FLOOD_PROGRAM_COMMITTEE))
            userProfile.Role = UserRoleEnum.PROGRAM_COMMITTEE;

        if (this.accessor.HttpContext.User.IsInRole(IdentityRoles.FLOOD_PROGRAM_READONLY))
            userProfile.Role = UserRoleEnum.PROGRAM_READONLY;

        if (this.accessor.HttpContext.User.IsInRole(IdentityRoles.SYSTEM_ADMIN))
            userProfile.Role = UserRoleEnum.SYSTEM_ADMIN;

        if (Role != UserRoleEnum.NONE)
            return;

        this.isExternalUser = true;

        agencyUserRoles = new List<AgencyUserRole>();
        userProfile.AgencyIds = new List<int>();
        int number;
        foreach (var c in this.accessor.HttpContext.User.Claims)
        {
            switch (c.Type)
            {
                case IdentityClaimTypes.FLOOD_AGENCY_ADMIN:
                    if (!string.IsNullOrEmpty(c.Value) && int.TryParse(c.Value, out number))
                    {
                        userProfile.AgencyIds.Add(number);
                        agencyUserRoles.Add(new AgencyUserRole() { AgencyId = number, UserRole = UserRoleEnum.AGENCY_ADMIN });
                    }

                    break;
                case IdentityClaimTypes.FLOOD_AGENCY_EDITOR:
                    if (!string.IsNullOrEmpty(c.Value) && int.TryParse(c.Value, out number))
                    {
                        userProfile.AgencyIds.Add(number);
                        agencyUserRoles.Add(new AgencyUserRole() { AgencyId = number, UserRole = UserRoleEnum.AGENCY_EDITOR });
                    }
                    break;
                case IdentityClaimTypes.FLOOD_AGENCY_SIGNATURE:
                    if (!string.IsNullOrEmpty(c.Value) && int.TryParse(c.Value, out number))
                    {
                        userProfile.AgencyIds.Add(number);
                        agencyUserRoles.Add(new AgencyUserRole() { AgencyId = number, UserRole = UserRoleEnum.AGENCY_SIGNATORY });
                    }
                    break;
                case IdentityClaimTypes.FLOOD_AGENCY_READONLY:
                    if (!string.IsNullOrEmpty(c.Value) && int.TryParse(c.Value, out number))
                    {
                        userProfile.AgencyIds.Add(number);
                        agencyUserRoles.Add(new AgencyUserRole() { AgencyId = number, UserRole = UserRoleEnum.AGENCY_READONLY });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}


