namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentQueryHandler : BaseHandler, IRequestHandler<GetPropertyDocumentQuery, IEnumerable<PropertyDocumentTypeViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IPropertyDocumentRepository repoDocument;
        private readonly IApplicationRepository repoApplication;
        public GetPropertyDocumentQueryHandler(
            IMapper mapper,
            IPropertyDocumentRepository repoDocument,
            IApplicationRepository repoApplication
            ) : base(repoApplication: repoApplication)
        {
            this.mapper = mapper;
            this.repoDocument = repoDocument;
            this.repoApplication = repoApplication;
        }
        public async Task<IEnumerable<PropertyDocumentTypeViewModel>> Handle(GetPropertyDocumentQuery request, CancellationToken cancellationToken)
        {
            // get application details
            var application = await GetIfApplicationExists(request.ApplicationId);

            var documents = await repoDocument.GetPropertyDocumentsAsync(application.Id, request.PamsPin, (int)PropertySectionEnum.OTHER_DOCUMENTS);
            var adminDocuments = await repoDocument.GetPropertyDocumentsAsync(application.Id, request.PamsPin, (int)PropertySectionEnum.ADMIN_DETAILS);
           
            List<PropertyDocumentTypeViewModel>? documentsTree = new List<PropertyDocumentTypeViewModel>();

            if (documents.Count() > 0)
            {
                var docBuilder = new PropertyDocumentTreeBuilder(documents);
                documentsTree.AddRange(docBuilder.DocumentsTree);
            }

            if (adminDocuments.Count() > 0)
            {
                var adminDocBuilder = new PropertyDocumentTreeBuilder(adminDocuments);
                documentsTree.AddRange(adminDocBuilder.DocumentsTree);
            }
            return documentsTree;
        }
    }
}

