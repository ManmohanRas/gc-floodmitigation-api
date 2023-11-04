namespace PresTrust.FloodMitigation.Application.Queries;

public class GetBrokenRulesQueryHandler: BaseHandler, IRequestHandler<GetBrokenRulesQuery, IEnumerable<GetBrokenRulesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IBrokenRuleRepository repoBrokenRule;
    private readonly IPresTrustUserContext userContext;

    public GetBrokenRulesQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IBrokenRuleRepository repoBrokenRule,
            IPresTrustUserContext userContext
            ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoBrokenRule = repoBrokenRule;
        this.userContext = userContext;
    }

    public async Task<IEnumerable<GetBrokenRulesQueryViewModel>> Handle(GetBrokenRulesQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        userContext.DeriveRole(application.AgencyId);
        bool isApplicantFlow = userContext.Role != UserRoleEnum.PROGRAM_ADMIN;

        // get broken rule details
        var brokenRules = await repoBrokenRule.GetBrokenRulesAsync(request.ApplicationId);
        if (isApplicantFlow)
        {
            brokenRules = brokenRules.Where(o => o.IsApplicantFlow).ToList();
        }

        var result = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<GetBrokenRulesQueryViewModel>>(brokenRules);

        return result;
    }
}
