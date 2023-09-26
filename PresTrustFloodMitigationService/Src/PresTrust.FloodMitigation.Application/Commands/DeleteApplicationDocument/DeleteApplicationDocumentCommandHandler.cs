namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class DeleteApplicationDocumentCommandHandler : IRequestHandler<DeleteApplicationDocumentCommand, bool>
{
    private readonly IApplicationDocumentRepository repoDocument;
    public DeleteApplicationDocumentCommandHandler
        (
        IApplicationDocumentRepository repoDocument
        )
    {
        this.repoDocument = repoDocument;
    }
    public async Task<bool> Handle(DeleteApplicationDocumentCommand request, CancellationToken cancellationToken)
    {
        // delete document
        await repoDocument.DeleteApplicationDocumentAsync(request.Id);

        return true;
    }
}
