using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;
public class PreservePropertyCommandHandler : BaseHandler, IRequestHandler<PreservePropertyCommand, PreservePropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPropertyDocumentRepository repoPropertyDocuments;
    private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRules;
    private readonly IPropertyAdminDetailsRepository repoPropertyAdminDetails;

    public PreservePropertyCommandHandler
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
    public async Task<PreservePropertyCommandViewModel> Handle(PreservePropertyCommand request, CancellationToken cancellationToken)
    {
        PreservePropertyCommandViewModel result = new ();

        // check if Property exists
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        // check if any broken rules exists, if yes then return
        var brokenRules = await repoPropertyBrokenRules.GetPropertyBrokenRulesAsync(property.ApplicationId, property.PamsPin);
        var hasOtherDocuments = await CheckPropertyOtherDocs(application.Id, application.StatusId, property.PamsPin, property.StatusId, (int)PropertySectionEnum.OTHER_DOCUMENTS);
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

        if (brokenRules != null && brokenRules.Any())
        {
            result.BrokenRules = mapper.Map<IEnumerable<FloodPropertyBrokenRuleEntity>, IEnumerable<PropertyBrokenRulesViewModel>>(brokenRules);
            return result;
        }

        //update Property
        if (property != null)
        {
            property.StatusId = (int)PropertyStatusEnum.PRESERVED;
            property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(property);
            FloodParcelStatusLogEntity appParcelStatusLog = new()
            {
                ApplicationId = property.ApplicationId,
                PamsPin = property.PamsPin,
                StatusId = property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appParcelStatusLog);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
    private async Task<bool> CheckPropertyOtherDocs(int applicationId, int applicationStatusId, string pamsPin, int propertyStatusId, int sectionId)
    {
        //var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
        //var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

        //FloodPropertyDocumentEntity docsSurveyLegalDescription = default;
        //FloodPropertyDocumentEntity docsSurveyReviewLetter= default;
        //FloodPropertyDocumentEntity docsHomeOwnerSurvey= default;
        //FloodPropertyDocumentEntity docsPreliminaryAssessmentReport= default;
        //FloodPropertyDocumentEntity docsTittleSearchReport = default; 
        //FloodPropertyDocumentEntity docsPreliminaryAssessmentReportReviewLetter = default; 

        //docsSurveyLegalDescription = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.SURVEY_LEGAL_DESCRIPTION).FirstOrDefault();
        //docsSurveyReviewLetter = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.SURVEY_REVIEW_LETTER).FirstOrDefault();
        //docsHomeOwnerSurvey = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.HOME_OWNERSURVEY).FirstOrDefault();
        //docsPreliminaryAssessmentReport = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT).FirstOrDefault();
        //docsTittleSearchReport = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.TITLE_SEARCH_REPORT).FirstOrDefault();
        //docsPreliminaryAssessmentReportReviewLetter = documents.Where(d => d.DocumentTypeId == (int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT_REVIEWIETTER).FirstOrDefault();

        //if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE && propertyStatusId == (int)PropertyStatusEnum.APPROVED)
        //{
        //    if (docsSurveyLegalDescription == null || docsTittleSearchReport == null)
        //    {

        //    }
        //    if (adminDetails.IsDEPInvolved == true)
        //    {
        //        docsSurveyReviewLetter == null
        //    }
        //    else 
        //    if (adminDetails.IsPARRequestedbyFunder == false)
        //    {
        //        docsHomeOwnerSurvey == null
        //    }
        //    else 
        //    if (adminDetails.IsPARRequestedbyFunder == true)
        //    {
        //        docsPreliminaryAssessmentReport == null || docsPreliminaryAssessmentReportReviewLetter == null
        //    }

        //}
        //return true;

        var requiredDocumentTypes = new List<int>();

        if (applicationStatusId == (int)ApplicationStatusEnum.ACTIVE)
        {
            var adminDetails = await repoPropertyAdminDetails.GetAsync(applicationId, pamsPin);

            switch (propertyStatusId)
            {
                case (int)PropertyStatusEnum.APPROVED:
                    requiredDocumentTypes = new List<int>() {
                       (int)PropertyDocumentTypeEnum.SURVEY_LEGAL_DESCRIPTION,
                       (int)PropertyDocumentTypeEnum.TITLE_SEARCH_REPORT,
                    };
                    if (adminDetails.IsDEPInvolved == true)
                    {
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.SURVEY_REVIEW_LETTER);
                    }
                    if (adminDetails.IsPARRequestedbyFunder == false)
                    {
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.HOME_OWNERSURVEY);
                    }
                    if (adminDetails.IsPARRequestedbyFunder == true)
                    {
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT);
                        requiredDocumentTypes.Add((int)PropertyDocumentTypeEnum.PRELIMINARY_ASSESSMENT_REPORT_REVIEWIETTER);
                    }
                    break;
            }

            var documents = await repoPropertyDocuments.GetPropertyDocumentsAsync(applicationId, pamsPin, sectionId);
            var savedDocumentTypes = documents.Where(o => requiredDocumentTypes.Contains(o.DocumentTypeId)).Select(o => o.DocumentTypeId).Distinct().ToList();

            return requiredDocumentTypes.Except(savedDocumentTypes).Count() == 0;
        }

        return true;
    }

}
