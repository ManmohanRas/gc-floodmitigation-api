namespace PresTrust.FloodMitigation.Application.Queries;

public class GetCountyUsersQueryHandler : IRequestHandler<GetCountyUsersQuery, IEnumerable<PresTrustUserEntity>>
{
    private readonly IMapper mapper;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IIdentityApiConnect identityApiConnect;
    private readonly IApplicationRepository repoApplication;

    public GetCountyUsersQueryHandler(
        IMapper mapper,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IIdentityApiConnect identityApiConnect
        )
    {
        this.mapper = mapper;
        this.systemParamOptions = systemParamOptions.Value;
        this.identityApiConnect = identityApiConnect;
    }
    public async Task<IEnumerable<PresTrustUserEntity>> Handle(GetCountyUsersQuery request, CancellationToken cancellationToken)
    {
        // get identity users by agency id
        var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood";
        var usersResult = await identityApiConnect.GetDataAsync<List<IdentityApiUser>>(endPoint);

        var countyUsers = mapper.Map<IEnumerable<IdentityApiUser>, IEnumerable<PresTrustUserEntity>>(usersResult);
        foreach (var item in countyUsers)
        {
            item.Status = item.IsEnabled ? "Active" : "In-Active";
        }
        return countyUsers;
    }
}
