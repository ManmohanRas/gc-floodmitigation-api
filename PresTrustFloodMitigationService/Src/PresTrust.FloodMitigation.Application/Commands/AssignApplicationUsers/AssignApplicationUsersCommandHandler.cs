namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class AssignApplicationUsersCommandHandler : BaseHandler, IRequestHandler<AssignApplicationUsersCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationUserRepository repoApplicationUser;
    private readonly IApplicationRepository repoApplication;
    private readonly IBrokenRuleRepository repoBrokenRules;
    
    public AssignApplicationUsersCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationUserRepository repoApplicationUser,
        IApplicationRepository repoApplication,
        IBrokenRuleRepository repoBrokenRules
        ): base (repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplicationUser = repoApplicationUser;
        this.repoApplication = repoApplication;
        this.repoBrokenRules = repoBrokenRules;
    }
    public async Task<Unit> Handle(AssignApplicationUsersCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var reqApplicationUsers = mapper.Map<IEnumerable<FloodApplicationUserViewModel>, IEnumerable<FloodApplicationUserEntity>>(request.ApplicationUsers).ToList();

        foreach (var applicationUser in reqApplicationUsers)
        {
            applicationUser.LastUpdatedBy = userContext.Email;
            applicationUser.ApplicationId = request.ApplicationId;
        }
        // returns broken rules  
        var brokenRules = ReturnBrokenRulesIfAny(application, reqApplicationUsers);


        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplicationUser.DeleteApplicationUsersByApplicationIdAsync(request.ApplicationId);
            List<FloodApplicationUserEntity> users = reqApplicationUsers.Where(au => (au.IsPrimaryContact) || (au.IsAlternateContact)).ToList();

            await repoApplicationUser.SaveAsync(users);

            if (brokenRules.Count() > 0)
                await repoBrokenRules.SaveBrokenRules(brokenRules);

            scope.Complete();
        }
        return Unit.Value;
    }

    /// <summary>
    /// Return broken rules in case of any business rule failure
    /// </summary>
    /// <param name="request"></param>
    /// <param name="application"></param>
    /// <returns></returns>
    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application, List<FloodApplicationUserEntity> reqApplicationUsers)
    {
        int sectionId = (int)ApplicationSectionEnum.ROLES;
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        var primaryContacts = reqApplicationUsers.Where(au => au.IsPrimaryContact).ToList();

        // empty primary contacts list
        if(application.Status == ApplicationStatusEnum.SUBMITTED) 
        {
            if (primaryContacts == null || primaryContacts.Count() == 0)
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "Primary Contact must be assigned to the application.",
                    IsApplicantFlow = true
                });
        }

        return brokenRules;
    }
}
