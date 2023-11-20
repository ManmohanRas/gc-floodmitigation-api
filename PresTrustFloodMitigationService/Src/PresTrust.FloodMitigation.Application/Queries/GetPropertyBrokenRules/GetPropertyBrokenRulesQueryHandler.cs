using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyBrokenRulesQueryHandler: BaseHandler, IRequestHandler<GetPropertyBrokenRulesQuery, IEnumerable<GetPropertyBrokenRulesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IPropertyBrokenRuleRepository repoBrokenRule;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationParcelRepository repoAppParcel;

    public GetPropertyBrokenRulesQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IPropertyBrokenRuleRepository repoBrokenRule,
            IPresTrustUserContext userContext,
            IApplicationParcelRepository repoAppParcel
            ) : base(repoApplication: repoApplication, repoProperty:repoAppParcel)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoBrokenRule = repoBrokenRule;
        this.userContext = userContext;
        this.repoAppParcel = repoAppParcel;
    }

    public async Task<IEnumerable<GetPropertyBrokenRulesQueryViewModel>> Handle(GetPropertyBrokenRulesQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var pamspin = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        userContext.DeriveRole(application.AgencyId);
        bool isPropertyFlow = userContext.Role != UserRoleEnum.PROGRAM_ADMIN;

        // get broken rule details
        var brokenRules = await repoBrokenRule.GetPropertyBrokenRulesAsync(request.ApplicationId,  request.PamsPin);
        if (isPropertyFlow)
        {
            brokenRules = brokenRules.Where(o => o.IsPropertyFlow).ToList();
        }

        var result = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<GetPropertyBrokenRulesQueryViewModel>>(brokenRules);

        return result;
    }
}
