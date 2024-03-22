namespace PresTrust.FloodMitigation.Application.Queries;

public class GetBrokenRulesQueryHandler: BaseHandler, IRequestHandler<GetBrokenRulesQuery, IEnumerable<GetBrokenRulesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IBrokenRuleRepository repoBrokenRule;
    private readonly IPresTrustUserContext userContext;
    private readonly IPropReleaseOfFundsRepository repoPropReleaseOfFunds;
    private readonly IPropertyBrokenRuleRepository repoPropBrokenRules;
    private readonly IParcelPropertyRepository repoParcelProperty;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;

    public GetBrokenRulesQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IApplicationParcelRepository repoApplicationParcel,
            IBrokenRuleRepository repoBrokenRule,
            IPresTrustUserContext userContext,
            IPropertyBrokenRuleRepository repoPropBrokenRules,
            IPropReleaseOfFundsRepository repoPropReleaseOfFunds,
            IPropertyDocumentRepository repoPropertyDocuments,
            IParcelPropertyRepository repoParcelProperty
            ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoApplicationParcel = repoApplicationParcel;
        this.repoBrokenRule = repoBrokenRule;
        this.userContext = userContext;
        this.repoPropReleaseOfFunds = repoPropReleaseOfFunds;
        this.repoPropBrokenRules = repoPropBrokenRules;
        this.repoPropertyDocuments = repoPropertyDocuments;
        this.repoParcelProperty = repoParcelProperty;
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
            if (application.Status == ApplicationStatusEnum.DRAFT)
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
                        Message = "All the Properties must be submitted",
                        IsApplicantFlow = true
                    });
                }
            }

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
                        Message = "All the Properties must be submitted",
                        IsApplicantFlow = true
                    }); 
                }
            }
            else if (application.Status == ApplicationStatusEnum.SUBMITTED)
            {
                var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
                foreach (var appParcel in appParcels)
                {
                    var propBrokenRules = (await repoPropBrokenRules.GetPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin))?.ToList();
                    if (propBrokenRules != null && propBrokenRules.Any())
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                            Message = "One or more properties are incomplete",
                            IsApplicantFlow = true
                        });
                        break;
                    }
                }
            }
            else if (application.Status == ApplicationStatusEnum.IN_REVIEW)
            {
                var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
                foreach (var appParcel in appParcels)
                {
                    var propBrokenRules = (await repoPropBrokenRules.GetPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin))?.ToList();
                    if (propBrokenRules != null && propBrokenRules.Any())
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                            Message = "One or more properties are incomplete",
                            IsApplicantFlow = true
                        });
                        break;
                    }
                }
            }
            else if(application.Status == ApplicationStatusEnum.ACTIVE)
            {
                var appParcels = await repoApplicationParcel.GetApplicationParcelsByApplicationIdAsync(application.Id);
                var requiredPropertyStatuses = new List<int>()
                {
                    (int)PropertyStatusEnum.PRESERVED,
                    (int)PropertyStatusEnum.WITHDRAWN,
                    (int)PropertyStatusEnum.PROJECT_AREA_EXPIRED,
                    (int)PropertyStatusEnum.GRANT_EXPIRED,
                    (int)PropertyStatusEnum.TRANSFERRED
                };
                if (appParcels.Where(o => !requiredPropertyStatuses.Contains(o.StatusId)).Count() > 0)
                {
                    brokenRules.Add(new FloodBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                        Message = "One or more properties have not reached final statuses",
                        IsApplicantFlow = true
                    });
                }

                appParcels = appParcels.Where(o => o.Status == PropertyStatusEnum.PRESERVED).ToList();
                foreach (var appParcel in appParcels)
                {
                    var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, appParcel.PamsPin, appParcel.StatusId);

                    var propBrokenRules = await repoPropBrokenRules.GetPropertyBrokenRulesAsync(application.Id, appParcel.PamsPin);
                    if (propBrokenRules.Count > 0 || !hasOtherDocuments)
                    {
                        brokenRules.Add(new FloodBrokenRuleEntity()
                        {
                            ApplicationId = application.Id,
                            SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                            Message = "One or more properties are incomplete",
                            IsApplicantFlow = true
                        });
                    }

                    //if (!hasOtherDocuments)
                    //{
                    //    brokenRules.Add(new FloodBrokenRuleEntity()
                    //    {
                    //        ApplicationId = application.Id,
                    //        SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                    //        Message = "One or more properties are incomplete",
                    //        IsApplicantFlow = true
                    //    });
                    //}

                    var parcelProperty = await repoParcelProperty.GetAsync(application.Id, appParcel.PamsPin);
                    if (parcelProperty != null && parcelProperty.NeedSoftCost)
                    {
                        var propertyPayment = await repoPropReleaseOfFunds.GetReleaseOfFundsAsync(application.Id, appParcel.PamsPin);
                        if (propertyPayment.SoftCostPaymentStatus != PaymentStatusEnum.FUNDS_RELEASED)
                        {
                            brokenRules.Add(new FloodBrokenRuleEntity()
                            {
                                ApplicationId = application.Id,
                                SectionId = (int)ApplicationSectionEnum.PROJECT_AREA,
                                Message = "Softcost payments for one or more properties are not released",
                                IsApplicantFlow = true
                            });
                        }
                    }
                }

            }

        }

        var result = mapper.Map<IEnumerable<FloodBrokenRuleEntity>, IEnumerable<GetBrokenRulesQueryViewModel>>(brokenRules);

        return result;
    }

    private async Task<bool> CheckPropertyOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId)
    {
        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE && propertyStatusId == (int)PropertyStatusEnum.PRESERVED)
        {
            var requiredDocumentTypes = new List<int>() {
                (int)PropertyDocumentTypeEnum.RECORDED_DEED,
                (int)PropertyDocumentTypeEnum.EXECUTED,
                (int)PropertyDocumentTypeEnum.TITLE_INSURANCE_POLICY
            };
            var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, (int)PropertySectionEnum.OTHER_DOCUMENTS);
            var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();
            return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
        }

        return true;
    }
}
