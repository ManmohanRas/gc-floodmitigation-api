namespace PresTrust.FloodMitigation.Application.Services.IdentityApi
{
    public class IdentityApiUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public bool IsEnabled { get; set; }
        public string UserRole
        {
            get
            {
                string result = string.Empty;
                if (this.Roles != null && Roles.Count() > 0)
                {
                    result = string.Join(",", this.Roles.Select(r => r.Name ?? ""));
                }

                if (this.Claims != null && this.Claims.Count() > 0)
                {
                    result = "";
                    result = string.Join(",", this.Claims.Select(c => c.ClaimType ?? ""));
                }
                return result;
            }
        }
        public List<IdentityUserClaim> Claims { get; set; }
        public List<IdentityUserRole> Roles { get; set; }
    }
}
