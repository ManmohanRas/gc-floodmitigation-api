namespace PresTrust.FloodMitigation.Application.Queries;

public class GetBrokenRulesQueryHandler: BaseHandler, IRequestHandler<GetBrokenRulesQuery, IEnumerable<GetBrokenRulesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IBrokenRuleRepository repoBrokenRule;
    private readonly IPresTrustUserContext userContext;

    public GetBrokenRulesQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IApplicationParcelRepository repoApplicationParcel,
            IBrokenRuleRepository repoBrokenRule,
            IPresTrustUserContext userContext
            ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
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
        else
        {
            if(application.Status == ApplicationStatusEnum.DRAFT)
            {
                bool hasNonSubmittedParcels = false;
                var parcels = await repoApplicationParcel.GetApplicationPropertiesAsync(request.ApplicationId);
                hasNonSubmittedParcels = parcels.Count(o => o.Status != PropertyStatusEnum.SUBMITTED) > 0;
                if (hasNonSubmittedParcels)
                {
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                        Message = "All the Properties must be submitted"
                    });
                }
            }
        }

        var result = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<GetBrokenRulesQueryViewModel>>(brokenRules);

        return result;
    }
}
