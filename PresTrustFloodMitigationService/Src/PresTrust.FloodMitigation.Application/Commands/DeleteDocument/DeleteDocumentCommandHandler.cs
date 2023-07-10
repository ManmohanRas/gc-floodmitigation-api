namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
{
    private readonly IDocumentRepository repoDocument;
    public DeleteDocumentCommandHandler
        (
        IDocumentRepository repoDocument
        )
    {
        this.repoDocument = repoDocument;
    }
    public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        // delete document
        await repoDocument.DeleteDocumentAsync(request.Id);

        return true;
    }
}
