namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDeclarationCommandHandler : BaseHandler, IRequestHandler<SaveDeclarationCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IParcelRepository repoParcel;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IApplicationUserRepository repoApplicationUser;
    private readonly IBrokenRuleRepository repoBrokenRule;
    public SaveDeclarationCommandHandler (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IParcelRepository repoParcel,
        IApplicationParcelRepository repoApplicationParcel,
        IApplicationUserRepository repoApplicationUser,
        IBrokenRuleRepository repoBrokenRule

        ) : base (repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoParcel = repoParcel;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoApplicationUser = repoApplicationUser;
        this.repoBrokenRule = repoBrokenRule;
    }
    public async Task<bool> Handle(SaveDeclarationCommand request, CancellationToken cancellationToken)
    {
        bool result = false;

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        var reqApp = mapper.Map<SaveDeclarationCommand, FloodApplicationEntity>(request);
        application.Title = reqApp.Title;
        application.AgencyId = reqApp.AgencyId;
        application.ApplicationSubTypeId = reqApp.ApplicationSubTypeId;

        //check for new parcels
        var newParcels = request.Parcels.Where(o => !o.IsValidPamsPin).Select(o => new FloodParcelEntity()
        {
            Id = o.Id,
            PamsPin = o.PamsPin,
            AgencyId = request.AgencyId,
            Block = o.Block,
            Lot = o.Lot,
            QCode = o.QCode,
            PropertyAddress = o.PropertyAddress,
            LandOwner = o.LandOwner,
        }).ToList();

        //update application parcels
        var reqAppParcels = request.Parcels.Select(o => new FloodApplicationParcelEntity() {
            ApplicationId = application.Id,
            PamsPin = o.PamsPin,
            Status = PropertyStatusEnum.NONE,
            IsLocked = false
        }).ToList();

        //update application users
        var reqAppUsers = mapper.Map<List<FloodApplicationUserViewModel>, List<FloodApplicationUserEntity>>(request.ApplicationUsers);
        reqAppUsers = reqAppUsers.Select(o => {
            o.ApplicationId = application.Id;
            o.LastUpdatedBy = userContext.Email;
            return o;
        }).Where(au => (au.IsPrimaryContact) || (au.IsAlternateContact)).ToList();

        // returns broken rules  
        var brokenRules = ReturnBrokenRulesIfAny(application, request, reqAppParcels?.Count ?? 0, reqAppUsers?.Count ?? 0);

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplication.SaveAsync(application);
            if(newParcels.Count > 0)
            {
                await repoParcel.SaveAsync(newParcels);
            }
            await repoApplicationParcel.DeleteApplicationParcelsByApplicationIdAsync(application.Id);
            await repoApplicationParcel.SaveAsync(reqAppParcels);
            await repoApplicationUser.DeleteApplicationUsersByApplicationIdAsync(application.Id);
            await repoApplicationUser.SaveAsync(reqAppUsers);

            await repoBrokenRule.DeleteBrokenRulesAsync(application.Id, ApplicationSectionEnum.DECLARATION_OF_INTENT);
            await repoBrokenRule.SaveBrokenRules(await brokenRules);

            scope.Complete();
            result = true;
        }


        return result;
    }

    private Task<List<FloodBrokenRuleEntity>> ReturnBrokenRulesIfAny(FloodApplicationEntity application, SaveDeclarationCommand request, int parcelCount, int rolesCount)
    {
        List<FloodBrokenRuleEntity> brokenRules = new List<FloodBrokenRuleEntity>();
        int sectionId = (int)ApplicationSectionEnum.DECLARATION_OF_INTENT;

        if (application.ApplicationType == ApplicationTypeEnum.CORE)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "Project Area title must be entered.",
                    IsApplicantFlow = true
                });

            }

            if (string.IsNullOrEmpty(request.ApplicationSubType)) 
            {
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "Sub Program type must be entered.",
                    IsApplicantFlow = true
                });
            }

            if (parcelCount == 0) 
            {
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "atleast one property is required.",
                    IsApplicantFlow = true
                });
            }

            if (rolesCount == 0) 
            {
                brokenRules.Add(new FloodBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    Message = "atleast one role is required.",
                    IsApplicantFlow = true
                });

            }    

        }

        return Task.FromResult(brokenRules);
    }
}
