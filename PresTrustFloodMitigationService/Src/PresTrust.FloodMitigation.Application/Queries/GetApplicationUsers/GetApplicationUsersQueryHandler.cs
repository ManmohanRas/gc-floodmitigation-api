namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetApplicationUsersQueryHandler : IRequestHandler<GetApplicationUsersQuery, IEnumerable<FloodApplicationUserViewModel>>
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
        IApplicationUserRepository repoApplicationUser
        )
    {
        this.mapper = mapper;
        this.systemParamOptions = systemParamOptions.Value;
        this.identityApiConnect = identityApiConnect;
        this.repoApplicationUser = repoApplicationUser;
    }

    public async Task<IEnumerable<FloodApplicationUserViewModel>> Handle(GetApplicationUsersQuery request, CancellationToken cancellationToken)
    {
        // get identity users by agency id
        var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood/1401";
        var usersResult = await identityApiConnect.GetDataAsync<List<IdentityApiUser>>(endPoint);
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
                        agencyUser.IsPrimaryContact =  pc.IsPrimaryContact;
                        agencyUser.IsAlternateContact = pc.IsAlternateContact;
                    }
                }
            }
        }
        return vmAgencyUsers;
    }
}
