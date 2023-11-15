namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyBrokenRulesQueryHandler: BaseHandler, IRequestHandler<GetPropertyBrokenRulesQuery, IEnumerable<GetPropertyBrokenRulesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IBrokenRuleRepository repoBrokenRule;
    private readonly IPresTrustUserContext userContext;

    public GetPropertyBrokenRulesQueryHandler(
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

    public async Task<IEnumerable<GetPropertyBrokenRulesQueryViewModel>> Handle(GetPropertyBrokenRulesQuery request, CancellationToken cancellationToken)
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

        var result = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<GetPropertyBrokenRulesQueryViewModel>>(brokenRules);

        return result;
    }
}
