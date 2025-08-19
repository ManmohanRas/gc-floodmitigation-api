namespace PresTrust.FloodMitigation.Application.Commands 
{ 
    public class DeletePropertyDocumentCommandHandler :BaseHandler, IRequestHandler<DeletePropertyDocumentCommand, bool>
    {
        private readonly IPropertyDocumentRepository propertyDocumentRepository;
        private readonly IApplicationRepository repoApplication;
        private readonly IPresTrustUserContext userContext;
        public DeletePropertyDocumentCommandHandler
            (
            IPropertyDocumentRepository propertyDocumentRepository,
            IApplicationRepository repoApplication,
            IPresTrustUserContext userContext
            ):base(repoApplication:repoApplication)
        {
            this.propertyDocumentRepository = propertyDocumentRepository;
            this.repoApplication = repoApplication;
            this.userContext = userContext;
        }
        public async Task<bool> Handle(DeletePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            userContext.DeriveUserProfileFromUserId(request.UserId);
            // delete document
            await propertyDocumentRepository.DeletePropertyDocumentAsync(request.Id);

            return true;
        }
    }
}
