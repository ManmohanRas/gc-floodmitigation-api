using static System.Formats.Asn1.AsnWriter;

namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveSignatoryCommandHandler : BaseHandler, IRequestHandler<SaveSignatoryCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackRepository repoFeedback;
    private ISignatoryRepository repoSignatory;
    private readonly IBrokenRuleRepository repoBrokenRules;


    public SaveSignatoryCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ISignatoryRepository repoSignatory,
        IFeedbackRepository repoFeedback,
        IBrokenRuleRepository repoBrokenRules
    ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSignatory = repoSignatory;
        this.repoFeedback = repoFeedback;
        this.repoBrokenRules = repoBrokenRules;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SaveSignatoryCommand request, CancellationToken cancellationToken)
    {
        int signatoryId = 0;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodSignatoryEntity
        var reqSignatory = mapper.Map<SaveSignatoryCommand, FloodSignatoryEntity>(request);

        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(reqSignatory);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.SIGNATORY);
            // Save current Broken Rules, if any
            await repoBrokenRules.SaveBrokenRules(brokenRules);

            var signatory = await repoSignatory.SaveAsync(reqSignatory);
            if (signatory != null)
            {
                signatoryId = signatory.Id;
            }

            scope.Complete();

        }

        return signatoryId;
    }

    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodSignatoryEntity reqSignature)
    {
        int sectionId = (int)ApplicationSectionEnum.SIGNATORY;
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        // add based on the empty check conditions
        if (string.IsNullOrEmpty(reqSignature.Designation))
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = reqSignature.ApplicationId,
                SectionId = sectionId,
                Message = "Designation required field on Signature tab have not been filled.",
                IsApplicantFlow = true
            }); ;

        if (string.IsNullOrEmpty(reqSignature.Title))
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = reqSignature.ApplicationId,
                SectionId = sectionId,
                Message = "Title required field on Signature tab have not been filled.",
                IsApplicantFlow = true
            }); ;

        if (reqSignature.SignedOn == null)
            brokenRules.Add(new FloodBrokenRuleEntity()
            {
                ApplicationId = reqSignature.ApplicationId,
                SectionId = sectionId,
                Message = "Date required field on Signature tab have not been filled.",
                IsApplicantFlow = true
            }); ;

        return brokenRules;
    }
    }
