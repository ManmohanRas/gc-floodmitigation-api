using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationOverviewCommandHandler : BaseHandler,IRequestHandler<SaveApplicationOverviewCommand, int>
{
    private readonly IMapper mapper;
    private IApplicationOverviewRepository repoOverviewDetails;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPresTrustUserContext userContext;
    private readonly IBrokenRuleRepository repoBrokenRules;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationFeedbackRepository repoFeedback;


    public SaveApplicationOverviewCommandHandler
        (
        IMapper mapper,
        IApplicationOverviewRepository repoOverviewDetails,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IPresTrustUserContext userContext,
        IBrokenRuleRepository repoBrokenRules,
        IApplicationRepository repoApplication,
        IApplicationFeedbackRepository repoFeedback
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoOverviewDetails = repoOverviewDetails;
        this.systemParamOptions = systemParamOptions.Value;
        this.userContext = userContext;
        this.repoBrokenRules = repoBrokenRules;
        this.repoApplication = repoApplication;
        this.repoFeedback = repoFeedback;
    }
    public async Task<int> Handle(SaveApplicationOverviewCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get feedback details and corrections
        IEnumerable<FloodApplicationFeedbackEntity> corrections = new List<FloodApplicationFeedbackEntity>();
        if (application.Status == ApplicationStatusEnum.SUBMITTED)
            corrections = await repoFeedback.GetFeedbacksAsync(request.ApplicationId, ApplicationCorrectionStatusEnum.REQUEST_SENT.ToString());
        AuthorizationCheck(application,corrections);

        var reqOverviewDetails = mapper.Map<SaveApplicationOverviewCommand,FloodApplicationOverviewEntity>(request);

        // Check Broken Rules
        var brokenRules = ReturnBrokenRulesIfAny(application, reqOverviewDetails);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            // Delete old Broken Rules, if any
            await repoBrokenRules.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.OVERVIEW);
            // Save current Broken Rules, if any
            await repoBrokenRules.SaveBrokenRules(brokenRules);
            reqOverviewDetails = await repoOverviewDetails.SaveAsync(reqOverviewDetails);
            reqOverviewDetails.LastUpdatedBy = userContext.Email;
            scope.Complete();

        }
        return reqOverviewDetails.Id;


    }

    /// <summary>
    /// Ensure that a user has the relevant authorizations to perform an action
    /// </summary>
    private void AuthorizationCheck(FloodApplicationEntity application, IEnumerable<FloodApplicationFeedbackEntity> corrections)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, application: application, operation: UserPermissionEnum.EDIT_OVERVIEW_SECTION, corrections.ToList());
    }


    private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application ,FloodApplicationOverviewEntity reqOverviewDetails)
    {
        int sectionId = (int)ApplicationSectionEnum.OVERVIEW;
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

        // add based on the empty check conditions
       
            if (reqOverviewDetails?.NatlDisaster == null)
            {
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "national distar required field on overview tab have not been filled.",
                    IsApplicantFlow = true
                });
            }

            if (reqOverviewDetails?.NatlDisaster == true)
            {
                if (string.IsNullOrEmpty(reqOverviewDetails.NatlDisasterName) || reqOverviewDetails.NatlDisasterYear == 0 || reqOverviewDetails.NatlDisasterMonth <= 0)
                {
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = sectionId,
                        Message = "Name,Month and year required field on overview tab have not been filled.",
                        IsApplicantFlow = true
                    });
                }
            }

            if (reqOverviewDetails?.LOI == null)
            {
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "LOI required field on overview tab have not been filled.",
                    IsApplicantFlow = true
                });
            }

            if (reqOverviewDetails?.LOI == true)
            {

                if (string.IsNullOrEmpty(reqOverviewDetails.LOIStatus))
                {
                     brokenRules.Add(new FloodBrokenRuleEntity()
                     {
                        ApplicationId = application.Id,
                        SectionId = sectionId,
                        Message = "LOI status required field on overview tab have not been filled.",
                        IsApplicantFlow = true
                     });
                }

            }

            if (reqOverviewDetails?.LOI == true)
            {
                if (reqOverviewDetails.LOIStatus == "Approved")
                {
                    if (reqOverviewDetails.LOIApprovedDate == null)
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = sectionId,
                            Message = "LOI Approved Date required field on overview tab have not been filled.",
                            IsApplicantFlow = true
                        });
                    }
                }
            }

        if (reqOverviewDetails.FEMA_OR_NJDEP_Applied == true)
        {
            if (reqOverviewDetails.FEMAApplied == true)
            {
                if (string.IsNullOrEmpty(reqOverviewDetails.FEMAStatus))
                {
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = sectionId,
                        Message = " FEMA status required field on overview tab have not been filled.",
                        IsApplicantFlow = true
                    });
                }

                if (reqOverviewDetails.FEMAStatus == "Approved")
                {
                    if (reqOverviewDetails.FEMAApprovedDate == null)
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = sectionId,
                            Message = " FEMA Approved Date required field on overview tab have not been filled.",
                            IsApplicantFlow = true
                        });

                    }
                }
            }

            if (reqOverviewDetails.GreenAcresApplied == true)
            {
                if (string.IsNullOrEmpty(reqOverviewDetails.GreenAcresStatus))
                {
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = sectionId,
                        Message = " green acers status required field on overview tab have not been filled.",
                        IsApplicantFlow = true
                    });
                }

                if (reqOverviewDetails.GreenAcresStatus == "Approved")
                {
                    if (reqOverviewDetails.GreenAcresApprovedDate == null)
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = sectionId,
                            Message = " Green acers Approved Date required field on overview tab have not been filled.",
                            IsApplicantFlow = true
                        });

                    }
                }
            }
             if (reqOverviewDetails.BlueAcresApplied == true)
             {
                if (string.IsNullOrEmpty(reqOverviewDetails.GreenAcresStatus))
                {
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = sectionId,
                        Message = " Blue acers status required field on overview tab have not been filled.",
                        IsApplicantFlow = true
                    });
                }

                if (reqOverviewDetails.BlueAcresStatus == "Approved")
                {
                    if (reqOverviewDetails.BlueAcresApprovedDate == null)
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = sectionId,
                            Message = " Blue acers Approved Date required field on overview tab have not been filled.",
                            IsApplicantFlow = true
                        });

                    }
                }

             }
        }

        return brokenRules;
    }
}
