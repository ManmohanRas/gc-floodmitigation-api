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

        public SavePropertyDocumentChecklistCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            IPropertyDocumentRepository repoDocument
        //ISiteRepository repoSite,
        // IBrokenRuleRepository repoBrokenRules
        ) : base(repoApplication: repoApplication)
        {
            this.mapper = mapper;
            this.userContext = userContext;
            this.systemParamOptions = systemParamOptions.Value;
            this.repoApplication = repoApplication;
            this.repoDocument = repoDocument;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(SavePropertyDocumentChecklistCommand request, CancellationToken cancellationToken)
        {
            // get application details
            var application = await GetIfApplicationExists(request.ApplicationId);

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

                scope.Complete();
            };

            return Unit.Value;
        }

    }
}
