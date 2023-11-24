using MediatR;
using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Commands;
public class ApprovePropertyCommandHandler : BaseHandler, IRequestHandler<ApprovePropertyCommand, ApprovePropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRules;
    private readonly IPropertyAdminDetailsRepository repoPropertyAdminDetails;


    public ApprovePropertyCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoProperty,
        IPropertyDocumentRepository repoPropertyDocuments,
        IPropertyBrokenRuleRepository repoPropertyBrokenRules,
        IPropertyAdminDetailsRepository repoPropertyAdminDetails

    ) : base(repoApplication, repoProperty)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoProperty = repoProperty;
        this.repoPropertyDocuments = repoPropertyDocuments;
        this.repoPropertyBrokenRules = repoPropertyBrokenRules;
        this.repoPropertyAdminDetails = repoPropertyAdminDetails;
    }

    /// <summary>
    /// 
    /// </summary>....................... 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ApprovePropertyCommandViewModel> Handle(ApprovePropertyCommand request, CancellationToken cancellationToken)
    {
        ApprovePropertyCommandViewModel result = new();


        // check if Property exists
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // check if any broken rules exists, if yes then return
        var brokenRules = await repoPropertyBrokenRules.GetPropertyBrokenRulesAsync(property.ApplicationId, property.PamsPin);
        var hasOtherDocuments = await CheckApplicationOtherDocs(application.Id, application.StatusId, property.PamsPin, property.StatusId, (int)PropertySectionEnum.OTHER_DOCUMENTS);
        if (!hasOtherDocuments)
        {
            brokenRules.Add(new FloodPropertyBrokenRuleEntity()
            {
                ApplicationId = application.Id,
                SectionId = (int)PropertySectionEnum.OTHER_DOCUMENTS,
                Message = "Required Documents are not uploaded in OtherDocuments Tab",
                IsPropertyFlow = true
            }); ;
        }

        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<PropertyBrokenRulesViewModel>>(brokenRules);
            return result;
        }


        //update Property
        if (property != null)
        {
            property.StatusId = (int)PropertyStatusEnum.APPROVED;
            property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(property);
            FloodParcelStatusLogEntity appStatusLog = new()
            {
                ApplicationId = property.ApplicationId,
                PamsPin = property.PamsPin,
                StatusId = property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appStatusLog);
            //change properties statuses to in-Pending in future

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }

    private async Task<bool> CheckApplicationOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId, int sectionId)
    {
        var requiredDocumentTypes = new int[] { };

        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE)
        { 
            var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

            switch (propertyStatusId)
            {
                case (int)PropertyStatusEnum.PENDING:
                    requiredDocumentTypes = new int[] {
                        (int)PropertyDocumentTypeEnum.APPRAISAL,
                        (int)PropertyDocumentTypeEnum.COUNTY_APPRAISAL_REPORT,//match and fast track option
                        (int)PropertyDocumentTypeEnum.VOLUNTARY_PARTICIPATION_FORM,
                        (int)PropertyDocumentTypeEnum.SETTLEMENT_SHEET,
                        (int)PropertyDocumentTypeEnum.FINAL_MITIGATION_OFFER,
                        (int)PropertyDocumentTypeEnum.MUNICIPAL_ORDINANCE_PURCHASE
                    };
                    if (adminDetails.DoesHomeOwnerHaveNFIPInsurance == true)
                    {
                        requiredDocumentTypes.Append((int)PropertyDocumentTypeEnum.DUPLICATION_BENEFITS_DOCUMENTS);
                    }
                    if (adminDetails.DoesHomeOwnerHaveNFIPInsurance == true)
                    {
                        requiredDocumentTypes.Append((int)PropertyDocumentTypeEnum.HOME_OWNER_AFFIDAVIT);
                    }
                    break;
                case (int)PropertyStatusEnum.APPROVED:
                    requiredDocumentTypes = new int[] {
                       (int)PropertyDocumentTypeEnum.SURVEY_LEGAL_DESCRIPTION,
                       (int)PropertyDocumentTypeEnum.TITLE_SEARCH_REPORT,
                    };
                    if (adminDetails.IsDEPInvolved == true)
                    {
                        requiredDocumentTypes.Append((int)PropertyDocumentTypeEnum.SURVEY_REVIEW_LETTER);
                    }
                    if (adminDetails.IsPARRequestedbyFunder == true)
                    {
                        requiredDocumentTypes.Append((int)PropertyDocumentTypeEnum.HOME_OWNERSURVEY);
                    }
                    if (adminDetails.IsPARRequestedbyFunder == true)
                    {
                        requiredDocumentTypes.Append((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT);
                    }
                    if (adminDetails.IsPARRequestedbyFunder == true)
                    {
                        requiredDocumentTypes.Append((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT_REVIEWIETTER);
                    }
                    break;
                case (int)PropertyStatusEnum.GRANT_EXPIRED:
                    requiredDocumentTypes = new int[] {
                       (int)PropertyDocumentTypeEnum.RECORDED_DEED,
                       (int)PropertyDocumentTypeEnum.EXECUTED,
                       (int)PropertyDocumentTypeEnum.TITLE_INSURANCE_POLICY
                    };
                    break;
            }

            var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
            var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToArray();

            return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
        }

        return true;
    }


}
