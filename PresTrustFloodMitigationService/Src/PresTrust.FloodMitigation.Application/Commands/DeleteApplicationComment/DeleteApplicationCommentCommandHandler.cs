namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationCommentCommandHandler : IRequestHandler<DeleteApplicationCommentCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationCommentRepository repoComment;
    private readonly IPresTrustUserContext userContext;

    public DeleteApplicationCommentCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationCommentRepository repoComment)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoComment = repoComment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteApplicationCommentCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        var reqComment = mapper.Map<DeleteApplicationCommentCommand, FloodApplicationCommentEntity>(request);
        await repoComment.DeleteCommentAsync(reqComment);
        return true;
    }
}
