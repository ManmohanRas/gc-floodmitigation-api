namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class AssignApplicationUsersCommandHandler : IRequestHandler<AssignApplicationUsersCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationUserRepository repoApplicationUser;
    public AssignApplicationUsersCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationUserRepository repoApplicationUser
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplicationUser = repoApplicationUser;
    }
    public async Task<Unit> Handle(AssignApplicationUsersCommand request, CancellationToken cancellationToken)
    {
        var reqApplicationUsers = mapper.Map<IEnumerable<FloodApplicationUserViewModel>, IEnumerable<FloodApplicationUserEntity>>(request.ApplicationUsers).ToList();

        foreach (var applicationUser in reqApplicationUsers)
        {
            applicationUser.LastUpdatedBy = userContext.Email;
            applicationUser.ApplicationId = request.ApplicationId;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplicationUser.DeleteApplicationUsersByApplicationIdAsync(request.ApplicationId);
            List<FloodApplicationUserEntity> users = reqApplicationUsers.Where(au => (au.IsPrimaryContact) || (au.IsAlternateContact)).ToList();

            await repoApplicationUser.SaveAsync(users);

            scope.Complete();
        }
        return Unit.Value;
    }
}
