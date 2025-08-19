namespace PresTrust.FloodMitigation.Application.Commands
{
    internal class SavePropertyDocumentDetailsCommandHandler :  IRequestHandler<SavePropertyDocumentDetailsCommand, SavePropertyDocumentDetailsCommandViewModel>
    {
        private readonly IMapper mapper;
        private readonly IPresTrustUserContext userContext;
        private readonly IPropertyDocumentRepository repoDocument;

        public SavePropertyDocumentDetailsCommandHandler
            (
             IMapper mapper,
             IPresTrustUserContext userContext,
             IPropertyDocumentRepository repoDocument
            )
        {
            this.mapper = mapper;
            this.userContext = userContext;
            this.repoDocument = repoDocument;
        }
        public async Task<SavePropertyDocumentDetailsCommandViewModel> Handle(SavePropertyDocumentDetailsCommand request, CancellationToken cancellationToken)
        {
            userContext.DeriveUserProfileFromUserId(request.UserId);
            // map command object to the HistDocumentEntity
            var reqDocument = mapper.Map<SavePropertyDocumentDetailsCommand, FloodPropertyDocumentEntity>(request);
            reqDocument.LastUpdatedBy = userContext.Email;

            var entityDocument = await repoDocument.SavePropertyDocumentDetailsAsync(reqDocument);

            // map entity object to the SaveDocumentCommandViewModel
            var Document = mapper.Map<FloodPropertyDocumentEntity, SavePropertyDocumentDetailsCommandViewModel>(entityDocument);

            return Document;
        }
    }
}
