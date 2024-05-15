using PresTrust.FloodMitigation.Domain.Enums;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyBrokenRulesQueryHandler : BaseHandler, IRequestHandler<GetPropertyBrokenRulesQuery, IEnumerable<GetPropertyBrokenRulesQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly IPropertyBrokenRuleRepository repoBrokenRule;
    private readonly IPresTrustUserContext userContext;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;
    private readonly IApplicationParcelRepository repoAppParcel;
    private readonly IPropertyAdminDetailsRepository repoPropertyAdminDetails;

    public GetPropertyBrokenRulesQueryHandler(
            IMapper mapper,
            IApplicationRepository repoApplication,
            IPropertyBrokenRuleRepository repoBrokenRule,
            IPresTrustUserContext userContext,
            IApplicationParcelRepository repoAppParcel,
            IPropertyDocumentRepository repoPropertyDocuments,
            IPropertyAdminDetailsRepository repoPropertyAdminDetails
            ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoBrokenRule = repoBrokenRule;
        this.userContext = userContext;
        this.repoPropertyDocuments = repoPropertyDocuments;
        this.repoPropertyAdminDetails = repoPropertyAdminDetails;
        this.repoAppParcel = repoAppParcel;
    }

    public async Task<IEnumerable<GetPropertyBrokenRulesQueryViewModel>> Handle(GetPropertyBrokenRulesQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        userContext.DeriveRole(application.AgencyId);
        bool isPropertyFlow = userContext.Role != UserRoleEnum.PROGRAM_ADMIN;

        // get broken rule details
        var brokenRules = await repoBrokenRule.GetPropertyBrokenRulesAsync(request.ApplicationId, request.PamsPin);
        if (isPropertyFlow)
        {
            brokenRules = brokenRules.Where(o => o.IsPropertyFlow).ToList();
        }
        else
        {
            if (property.Status == PropertyStatusEnum.PENDING)
            {
                var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, property.PamsPin, property.StatusId, (int)PropertySectionEnum.OTHER_DOCUMENTS, application.ApplicationTypeId);
                if (!hasOtherDocuments)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = (int)PropertySectionEnum.OTHER_DOCUMENTS,
                        PamsPin = property.PamsPin,
                        Message = "Required Documents are not uploaded in OtherDocuments Tab",
                        IsPropertyFlow = true
                    });
                }
            }
            else if (property.Status == PropertyStatusEnum.APPROVED)
            {
                var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, property.PamsPin, property.StatusId, (int)PropertySectionEnum.OTHER_DOCUMENTS, application.ApplicationTypeId);
                if (!hasOtherDocuments)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = (int)PropertySectionEnum.OTHER_DOCUMENTS,
                        PamsPin = property.PamsPin,
                        Message = "Required Documents are not uploaded in OtherDocuments Tab",
                        IsPropertyFlow = true
                    });

                }

            }
            else if (property.Status == PropertyStatusEnum.PRESERVED)
            {
                var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, property.PamsPin, property.StatusId, (int)PropertySectionEnum.OTHER_DOCUMENTS, application.ApplicationTypeId);
                if (!hasOtherDocuments)
                {
                    brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                    {
                        ApplicationId = application.Id,
                        SectionId = (int)PropertySectionEnum.OTHER_DOCUMENTS,
                        PamsPin = property.PamsPin,
                        Message = "Required Documents are not uploaded in OtherDocuments Tab",
                        IsPropertyFlow = true
                    });

                }

            }
        }
        var result = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<GetPropertyBrokenRulesQueryViewModel>>(brokenRules);
        return result;
    }

    private async Task<bool> CheckPropertyOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId, int sectionId, int applicationTypeId)
    {
        var requiredDocumentTypes = new List<int>();

        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE)
        {
            if (propertyStatusId == (int)PropertyStatusEnum.PENDING)
            {
                var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

                switch (propertyStatusId)
                {
                    case (int)PropertyStatusEnum.PENDING:
                        requiredDocumentTypes = new List<int>() {
                                (int)PropertyDocumentTypeEnum.APPRAISAL,
                                (int)PropertyDocumentTypeEnum.VOLUNTARY_PARTICIPATION_FORM,
                                (int)PropertyDocumentTypeEnum.SETTLEMENT_SHEET,
                                (int)PropertyDocumentTypeEnum.FINAL_MITIGATION_OFFER,
                                (int)PropertyDocumentTypeEnum.MUNICIPAL_ORDINANCE_PURCHASE
                            };
                        if (applicationTypeId == (int)ApplicationTypeEnum.CORE)
                        {
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.COUNTY_APPRAISAL_REPORT);
                        }
                        if (adminDetails.DoesHomeOwnerHaveNFIPInsurance == true)
                        {
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.DUPLICATION_BENEFITS_DOCUMENTS);
                        }
                        if (adminDetails.DoesHomeOwnerHaveNFIPInsurance == true)
                        {
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.HOMEOWNER_AFFIDAVIT);
                        }
                        break;
                }

                var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
                var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();

                return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
            }
            else if (propertyStatusId == (int)PropertyStatusEnum.APPROVED)
            {
                var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

                switch (propertyStatusId)
                {
                    case (int)PropertyStatusEnum.APPROVED:
                        requiredDocumentTypes = new List<int>() {
                                (int)PropertyDocumentTypeEnum.SURVEY_LEGAL_DESCRIPTION,
                                (int)PropertyDocumentTypeEnum.TITLE_SEARCH_REPORT,
                            };
                        if (adminDetails.IsDEPInvolved)
                        {
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.SURVEY_REVIEW_LETTER);
                        }
                        if (adminDetails.IsPARRequestedbyFunder)
                        {
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT);
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT_REVIEWIETTER);
                        }
                        else
                        {
                            requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.HOMEOWNER_SURVEY);
                        }
                        break;
                }

                var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
                var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();

                return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
            }
            if (propertyStatusId == (int)PropertyStatusEnum.PRESERVED)
            {
                var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

                switch (propertyStatusId)
                {
                    case (int)PropertyStatusEnum.PRESERVED:
                        requiredDocumentTypes = new List<int>() {
                                (int)PropertyDocumentTypeEnum.RECORDED_DEED,
                                (int)PropertyDocumentTypeEnum.EXECUTED_HUD1,
                                (int)PropertyDocumentTypeEnum.TITLE_INSURANCE_POLICY
                        };
                        break;
                }

                var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
                var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();

                return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
            }
        }

        return true;
    }
}
