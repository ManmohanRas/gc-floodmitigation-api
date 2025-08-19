namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class DeleteApplicationDocumentCommandHandler : IRequestHandler<DeleteApplicationDocumentCommand, bool>
{
    private readonly IApplicationDocumentRepository repoDocument;
    private readonly IPresTrustUserContext userContext;
    public DeleteApplicationDocumentCommandHandler
        (
        IApplicationDocumentRepository repoDocument
        , IPresTrustUserContext userContext
        )
    {
        this.repoDocument = repoDocument;
        this.userContext = userContext;
    }
    public async Task<bool> Handle(DeleteApplicationDocumentCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        // delete document
        await repoDocument.DeleteApplicationDocumentAsync(request.Id);

        return true;
    }
}
