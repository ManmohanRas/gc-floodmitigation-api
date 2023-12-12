namespace PresTrust.FloodMitigation.Application.Commands;

public class CountyUserRoleChangeRequestCommandHandler : BaseHandler, IRequestHandler<CountyUserRoleChangeRequestCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IIdentityApiConnect identityApiConnect;
    private readonly IApplicationRepository repoApplication;

    public CountyUserRoleChangeRequestCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IIdentityApiConnect identityApiConnect,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.identityApiConnect = identityApiConnect;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(CountyUserRoleChangeRequestCommand request, CancellationToken cancellationToken)
    {
        JsonContent postUserJson = default;
        if (string.IsNullOrEmpty(request.Role))
        {
            request.Role = request.NewRole;
            request.NewRole = null;

            // call external api - IdentityApi
            postUserJson = new JsonContent(new PostUserRoleRequest()
            {
                Email = request.Email,
                Roles = new string[] { request.Role }
            });

            await this.identityApiConnect.PostDataAsync<string, JsonContent>($"{systemParamOptions.IdentityApiSubDomain}/UserAdmin", postUserJson);
        }
        else
        {
            // call external api - IdentityApi
            postUserJson = new JsonContent(new PutCountyUserRoleChangeRequest()
            {
                Email = request.Email,
                Role = request.Role,
                NewRole = request.NewRole
            });

            await this.identityApiConnect.PutDataAsync<string, JsonContent>($"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/user-role", postUserJson);
        }

        return true;
    }
}
