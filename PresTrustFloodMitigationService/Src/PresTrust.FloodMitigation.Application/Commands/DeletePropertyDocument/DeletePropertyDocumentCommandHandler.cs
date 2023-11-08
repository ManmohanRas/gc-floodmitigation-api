namespace PresTrust.FloodMitigation.Application.Commands 
{ 
    public class DeletePropertyDocumentCommandHandler :BaseHandler, IRequestHandler<DeletePropertyDocumentCommand, bool>
    {
        private readonly IPropertyDocumentRepository propertyDocumentRepository;
        private readonly IApplicationRepository repoApplication;
        public DeletePropertyDocumentCommandHandler
            (
            IPropertyDocumentRepository propertyDocumentRepository,
            IApplicationRepository repoApplication    
            ):base(repoApplication:repoApplication)
        {
            this.propertyDocumentRepository = propertyDocumentRepository;
            this.repoApplication = repoApplication;
        }
        public async Task<bool> Handle(DeletePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            // delete document
            await propertyDocumentRepository.DeletePropertyDocumentAsync(request.Id);

            return true;
        }
    }
}
