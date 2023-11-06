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

            Enum.TryParse(value: request.SectionName, ignoreCase: true, out PropertySectionEnum propertySection);
            var documents = await repoDocument.GetPropertyDocumentsAsync(application.Id, (int)propertySection,request.Pamspin);

            List<PropertyDocumentTypeViewModel>? documentsTree = default;

            if (documents.Count() > 0)
            {
                var docBuilder = new PropertyDocumentTreeBuilder(documents);
                documentsTree = docBuilder.DocumentsTree;
            }
            else
            {
                documentsTree = new List<PropertyDocumentTypeViewModel>();
            }

            return documentsTree;
        }
    }
}

