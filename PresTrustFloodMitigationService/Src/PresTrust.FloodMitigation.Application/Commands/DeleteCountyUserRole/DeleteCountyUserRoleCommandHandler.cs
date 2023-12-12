namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteCountyUserRoleCommandHandler : IRequestHandler<DeleteCountyUserRoleCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IIdentityApiConnect identityApiConnect;

    public DeleteCountyUserRoleCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IIdentityApiConnect identityApiConnect
        )
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
    public async Task<bool> Handle(DeleteCountyUserRoleCommand request, CancellationToken cancellationToken)
    {
        // call external api - IdentityApi
        var postUserJson = new JsonContent(new DeleteCountyUserRoleRequest()
        {
            Email = request.Email,
            Role = request.Role
        });

        var result = await this.identityApiConnect.PostDataAsync<string, JsonContent>($"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/user-role/delete", postUserJson);

        return true;
    }
}
