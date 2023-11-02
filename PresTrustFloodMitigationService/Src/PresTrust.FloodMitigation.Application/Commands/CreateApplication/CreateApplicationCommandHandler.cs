using MediatR;
using PresTrust.FloodMitigation.Application.Services.IdentityApi;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class CreateApplicationCommandHandler : BaseHandler, IRequestHandler<CreateApplicationCommand, CreateApplicationCommandViewModel>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IIdentityApiConnect identityApiConnect;
    private readonly IApplicationUserRepository repoApplicationUser;
    private readonly IBrokenRuleRepository repoBrokenRules;

    public CreateApplicationCommandHandler(
        IMapper  mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IIdentityApiConnect identityApiConnect,
        IApplicationUserRepository repoApplicationUser,
        IBrokenRuleRepository repoBrokenRules
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.identityApiConnect = identityApiConnect;
        this.repoApplicationUser = repoApplicationUser;
        this.repoBrokenRules = repoBrokenRules; 
    }

    public async Task<CreateApplicationCommandViewModel> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        //check permissions
        AuthorizationCheck(request);

        // create application
        var reqApplication = mapper.Map<CreateApplicationCommand, FloodApplicationEntity>(request);
        reqApplication.Status = ApplicationStatusEnum.DOI_DRAFT;
        reqApplication.CreatedByProgramAdmin = userContext.Role == UserRoleEnum.PROGRAM_ADMIN;
        reqApplication.LastUpdatedBy = userContext.Email;

        // check if any broken rules exists, if yes then return
        //var brokenRules = await repoBrokenRules.GetBrokenRulesAsync(reqApplication.Id);

        //// returns broken rules  
        //var defaultBrokenRules = ReturnBrokenRulesIfAny(reqApplication);
        //// save broken rules
        //await repoBrokenRules.SaveBrokenRules(defaultBrokenRules);


        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            reqApplication = await repoApplication.SaveAsync(reqApplication);

            FloodApplicationStatusLogEntity appStatusLog = new()
            {
                ApplicationId = reqApplication.Id,
                StatusId = reqApplication.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = reqApplication.LastUpdatedBy
            };
            await repoApplication.SaveStatusLogAsync(appStatusLog);

            var agencyUsers = await GetApplicationUsers(reqApplication.Id, reqApplication.AgencyId);
            if(agencyUsers?.Count() > 0)
            {
                var agencyAdminUser = agencyUsers.Where(o => o.Role == "flood_agencyadmin").FirstOrDefault();
                var agencyAdmin = mapper.Map<FloodApplicationUserViewModel, FloodApplicationUserEntity>(agencyAdminUser);
                if (agencyAdmin != null)
                {
                    agencyAdmin.ApplicationId = reqApplication.Id;
                    agencyAdmin.IsPrimaryContact = true;
                    await repoApplicationUser.SaveAsync(new List<FloodApplicationUserEntity>() { agencyAdmin });
                }
            }

            scope.Complete();
        }

        // get application details
        var application = await GetIfApplicationExists(reqApplication.Id);
        var result = mapper.Map<FloodApplicationEntity, CreateApplicationCommandViewModel>(application);

        // apply security
        FloodApplicationSecurityManager securityMgr = default;
        // derive user's role for a given agency
        userContext.DeriveRole(application.AgencyId);
        // derive navigation items & permissions
        switch (application.Status)
        {
            case ApplicationStatusEnum.DOI_SUBMITTED:
            case ApplicationStatusEnum.SUBMITTED:
            case ApplicationStatusEnum.IN_REVIEW:
            case ApplicationStatusEnum.ACTIVE:
                var feedbacksReqForCorrections = application.Feedbacks.Where(f => f.RequestForCorrection == true && string.Compare(f.CorrectionStatus, ApplicationCorrectionStatusEnum.REQUEST_SENT.ToString(), true) == 0).ToList();
                securityMgr = new FloodApplicationSecurityManager(userContext.Role, application.Status, application.PrevStatus, feedbacksReqForCorrections);
                break;
            default:
                securityMgr = new FloodApplicationSecurityManager(userContext.Role, application.Status, application.PrevStatus);
                break;
        }

        result.Permission = securityMgr.Permission;
        result.NavigationItems = securityMgr.NavigationItems;
        result.AdminNavigationItems = securityMgr.AdminNavigationItems;
        result.PostApprovedNavigationItems = securityMgr.PostApprovedNavigationItems;
        result.DefaultNavigationItem = securityMgr.DefaultNavigationItem;
        return result;
    }

    private void AuthorizationCheck(CreateApplicationCommand request)
    {
        userContext.DeriveRole(request.AgencyId);
        IsAuthorizedOperation(userRole: userContext.Role, applicationStatus: ApplicationStatusEnum.NONE, applicationPrevStatus: ApplicationStatusEnum.NONE, operation: UserPermissionEnum.CREATE_APPLICATION);
    }

    private async Task<IEnumerable<FloodApplicationUserViewModel>> GetApplicationUsers(int applicationId, int agencyId)
    {
        try
        {
            // get identity users by agency id
            var endPoint = $"{systemParamOptions.IdentityApiSubDomain}/UserAdmin/users/pres-trust/flood/{agencyId}";
            var usersResult = await identityApiConnect.GetDataAsync<List<IdentityApiUser>>(endPoint);
            var vmAgencyUsers = mapper.Map<IEnumerable<IdentityApiUser>, IEnumerable<FloodApplicationUserViewModel>>(usersResult);

            var applicationUsers = await repoApplicationUser.GetApplicationUsersAsync(applicationId);
            var vmApplicationUsers = mapper.Map<IEnumerable<FloodApplicationUserEntity>, IEnumerable<FloodApplicationUserViewModel>>(applicationUsers);

            if (vmApplicationUsers != null && vmApplicationUsers.Count() > 0)
            {
                foreach (var pc in vmApplicationUsers)
                {
                    foreach (var agencyUser in vmAgencyUsers)
                    {
                        if (string.Compare(agencyUser.Email, pc.Email, true) == 0)
                        {
                            agencyUser.Id = pc.Id;
                            agencyUser.IsPrimaryContact = pc.IsPrimaryContact;
                            agencyUser.IsAlternateContact = pc.IsAlternateContact;
                        }
                    }
                }
            }
            return vmAgencyUsers;
        }
        catch (Exception ex)
        {
            return new List<FloodApplicationUserViewModel>();
        }
    }
    //private List<FloodBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application)
    //{
    //    List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();

    //    // add default broken rule while initiating application flow
    //    brokenRules.Add(new FloodBrokenRuleEntity()
    //    {
    //        ApplicationId = application.Id,
    //        SectionId = (int)ApplicationSectionEnum.DECLARATION_OF_INTENT,
    //        Message = "All required fields on DOI tab have not been filled.",
    //        IsApplicantFlow = true
    //    });
    //    return brokenRules;
    //}
}
