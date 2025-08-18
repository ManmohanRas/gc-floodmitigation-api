namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetApplicationUsersQueryHandler : BaseHandler, IRequestHandler<GetApplicationUsersQuery, IEnumerable<FloodApplicationUserViewModel>>
{
    private IMapper mapper;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IIdentityApiConnect identityApiConnect;
    private readonly IApplicationUserRepository repoApplicationUser;

    public GetApplicationUsersQueryHandler
        (
        IMapper mapper,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IIdentityApiConnect identityApiConnect,
        IApplicationRepository repoApplication,
        IApplicationUserRepository repoApplicationUser
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.systemParamOptions = systemParamOptions.Value;
        this.identityApiConnect = identityApiConnect;
        this.repoApplicationUser = repoApplicationUser;
    }

    public async Task<IEnumerable<FloodApplicationUserViewModel>> Handle(GetApplicationUsersQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        try
        {
            // get identity users by agency id
            var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood/{request.AgencyId ?? application.AgencyId}";
            var usersResult = new List<IdentityApiUser>() {
                new IdentityApiUser() {   UserId =  "1" , UserName =  "Agencyadmin1401", Email =  "agencyadmin1401@gmail.com", PhoneNumber =  "(973) 999-9999", Title =  "",
                    Roles = new List<IdentityUserRole>() { new IdentityUserRole() { Name ="flood_agencyadmin"} }
                },
                new IdentityApiUser() {
                     UserId =  "2",
                     UserName =  "Consultant",
                     Email =  "consultant1@gmail.com",
                     PhoneNumber =  "",
                     Roles = new List<IdentityUserRole>() { new IdentityUserRole() { Name = "flood_agencyeditor" } }
                },
                new IdentityApiUser() {
                     UserId =  "3",
                     UserName =  "Planning Director",
                     Email =  "adirector@mytown.com",
                     PhoneNumber = "",
                     Roles = new List<IdentityUserRole>() { new IdentityUserRole() { Name = "flood_agencyreadonly" } }
                },
                new IdentityApiUser() {
                    UserId =  "4",
                    UserName =  "Agencysignatory1401",
                    Email =  "agencysignatory1401@gmail.com",
                    PhoneNumber =  "(973) 999-9999",
                    Roles = new List<IdentityUserRole>() { new IdentityUserRole() { Name = "flood_agencysignature" } }
                }
                };
            var vmAgencyUsers = mapper.Map<IEnumerable<IdentityApiUser>, IEnumerable<FloodApplicationUserViewModel>>(usersResult);

            var applicationUsers = await repoApplicationUser.GetApplicationUsersAsync(request.ApplicationId);
            var vmApplicationUsers = mapper.Map<IEnumerable<FloodApplicationUserEntity>, IEnumerable<FloodApplicationUserViewModel>>(applicationUsers);

            if (vmApplicationUsers != null && vmApplicationUsers.Count() > 0)
            {
                foreach (var pc in vmApplicationUsers)
                {
                    foreach (var agencyUser in vmAgencyUsers)
                    {
                        if (string.Compare(agencyUser.Email, pc.Email, true) == 0)
                        {
                            agencyUser.Id = pc.Id;
                            agencyUser.IsPrimaryContact = pc.IsPrimaryContact;
                            agencyUser.IsAlternateContact = pc.IsAlternateContact;
                        }
                    }
                }
            }
            return vmAgencyUsers;
        }
        catch (Exception ex)
        {
            return new List<FloodApplicationUserViewModel>();
        }
        
    }
}
