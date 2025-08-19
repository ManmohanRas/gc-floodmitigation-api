namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropertyCommentCommandHandler : IRequestHandler<DeletePropertyCommentCommand, bool>
{
    private readonly IMapper mapper;
    private readonly ICommentPropRepository repoComment;
    private readonly IPresTrustUserContext userContext;


    public DeletePropertyCommentCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        ICommentPropRepository repoComment)
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
    public async Task<bool> Handle(DeletePropertyCommentCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // map command object to the HistCommentsEntity
        var reqComment = mapper.Map<DeletePropertyCommentCommand, FloodPropertyCommentEntity>(request);

        // delete comment
        await repoComment.DeleteCommentAsync(reqComment);

        return true;
    }
}
