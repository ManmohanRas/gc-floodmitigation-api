namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetPropertyDocumentQueryHandler : BaseHandler, IRequestHandler<GetPropertyDocumentQuery, IEnumerable<PropertyDocumentTypeViewModel>>
    {
        private readonly IMapper mapper;
        private readonly IPropertyDocumentRepository repoDocument;
        private readonly IApplicationRepository repoApplication;
        private readonly IApplicationParcelRepository repoAppParcel;
        private readonly IPresTrustUserContext userContext;

        public GetPropertyDocumentQueryHandler(
            IMapper mapper,
            IPresTrustUserContext userContext,
            IPropertyDocumentRepository repoDocument,
            IApplicationRepository repoApplication,
            IApplicationParcelRepository repoAppParcel
            ) : base(repoApplication: repoApplication, repoProperty: repoAppParcel)
        {
            this.mapper = mapper;
            this.userContext = userContext;
            this.repoDocument = repoDocument;
            this.repoApplication = repoApplication;
            this.repoAppParcel = repoAppParcel;
        }
        public async Task<IEnumerable<PropertyDocumentTypeViewModel>> Handle(GetPropertyDocumentQuery request, CancellationToken cancellationToken)
        {
            userContext.DeriveUserProfileFromUserId(request.UserId);

            // get application details
            var application = await GetIfApplicationExists(request.ApplicationId);
            var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

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

