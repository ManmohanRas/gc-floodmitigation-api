namespace PresTrust.FloodMitigation.Application.Commands 
{ 
    public class DeletePropertyDocumentCommandHandler : IRequestHandler<DeletePropertyDocumentCommand, bool>
    {
        private readonly IPropertyDocumentRepository propertyDocumentRepository;
        public DeletePropertyDocumentCommandHandler
            (
            IPropertyDocumentRepository propertyDocumentRepository
            )
        {
            this.propertyDocumentRepository = propertyDocumentRepository;
        }
        public async Task<bool> Handle(DeletePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            // delete document
            await propertyDocumentRepository.DeletePropertyDocumentAsync(request.Id);

            return true;
        }
    }
}
