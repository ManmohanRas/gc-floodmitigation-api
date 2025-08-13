using AutoMapper;

namespace PresTrust.FloodMitigation.Application.Commands
{
    public class SavePropertyDocumentChecklistCommandHandler : BaseHandler, IRequestHandler<SavePropertyDocumentChecklistCommand, Unit>
    {


        private IMapper mapper;
        private readonly IPresTrustUserContext userContext;
        private readonly SystemParameterConfiguration systemParamOptions;
        private readonly IApplicationRepository repoApplication;
        private readonly IPropertyDocumentRepository repoDocument;
        private readonly IPropertyBrokenRuleRepository repoPropertyBrokenRules;
        private readonly IApplicationParcelRepository repoProperty;

        public SavePropertyDocumentChecklistCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            IPropertyDocumentRepository repoDocument,
            IPropertyBrokenRuleRepository repoPropertyBrokenRules,
            IApplicationParcelRepository repoProperty
        ) : base(repoApplication: repoApplication, repoProperty: repoProperty)
        {
            this.mapper = mapper;
            this.userContext = userContext;
            this.systemParamOptions = systemParamOptions.Value;
            this.repoApplication = repoApplication;
            this.repoDocument = repoDocument;
            this.repoPropertyBrokenRules = repoPropertyBrokenRules;
            this.repoProperty = repoProperty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SavePropertyDocumentChecklistCommand request, CancellationToken cancellationToken)
        {
            userContext.DeriveUserProfileFromUserId(request.UserId);

            // get application details
            var application = await GetIfApplicationExists(request.ApplicationId);
            var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

           // var brokenRules = await repoPropertyBrokenRules(property.ApplicationId, property.PamsPin);
            var brokenRules = ReturnBrokenRulesIfAny(application, request,property);

            // consider only add/updated records
            var viewmodelDocuments = request.Documents.Where(doc => string.Compare(doc.RowStatus, "U", ignoreCase: true) == 0).ToList();

            // map command object to the HistDocumentEntity
            var entityDocuments = mapper.Map<IEnumerable<PropertyDocumentViewModel>, IEnumerable<FloodPropertyDocumentEntity>>(viewmodelDocuments);

            // save application documents, property documents (review/checklist items)
            using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
            {
                foreach (var doc in entityDocuments)
                {
                    await repoDocument.SavePropertyDocumentChecklistAsync(doc);
                }
               await repoPropertyBrokenRules.DeletePropertyBrokenRulesAsync(application.Id, PropertySectionEnum.ADMIN_DOCUMENT_CHECKLIST, property.PamsPin);
               await repoPropertyBrokenRules.SavePropertyBrokenRules( brokenRules);

                scope.Complete();
            };

            return Unit.Value;
        }
        /// <summary>
        /// Return broken rules in case of any business rule failure
        /// </summary>
        /// <param name="application"></param>
        /// <param name="property"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        private List<FloodPropertyBrokenRuleEntity> ReturnBrokenRulesIfAny(FloodApplicationEntity application, SavePropertyDocumentChecklistCommand request, FloodApplicationParcelEntity property)
        {
            int sectionId = (int)PropertySectionEnum.ADMIN_DOCUMENT_CHECKLIST;
            List<FloodPropertyBrokenRuleEntity> brokenRules = new List<FloodPropertyBrokenRuleEntity>();

            // map command object to the FloodPropertyDocumentEntity
            var documents = mapper.Map<IEnumerable<PropertyDocumentViewModel>, IEnumerable<FloodPropertyDocumentEntity>>(request.Documents);
            var unapprovedDocs = documents.Where(doc => doc.Approved == false).ToList();
            if (documents == null || documents.Count() == 0)
            {
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    PamsPin = property.PamsPin,
                    Message = "All required documents (Admin-Document-property-Checklist) are not yet uploaded for committee review.",
                    IsPropertyFlow = false
                });
            }

            if (unapprovedDocs != null && unapprovedDocs.Count() > 0)
            {
                brokenRules.Add(new FloodPropertyBrokenRuleEntity()
                {
                    ApplicationId = application.Id,
                    SectionId = sectionId,
                    PamsPin = property.PamsPin,
                    Message = "All required documents (Admin-Document-property-Checklist) are not yet approved for committee review.",
                    IsPropertyFlow = false
                });
            }

            return brokenRules;
        }

    }
}

