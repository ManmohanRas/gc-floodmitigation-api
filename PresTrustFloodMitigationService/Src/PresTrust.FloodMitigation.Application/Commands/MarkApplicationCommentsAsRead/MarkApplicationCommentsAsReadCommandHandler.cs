namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkApplicationCommentsAsReadCommandHandler : IRequestHandler<MarkApplicationCommentsAsReadCommand, bool>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationCommentRepository repoComment;

    public MarkApplicationCommentsAsReadCommandHandler(IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationCommentRepository repoComment
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoComment = repoComment;
    }
    public async Task<bool> Handle(MarkApplicationCommentsAsReadCommand request, CancellationToken cancellationToken)
    {
        if (userContext.Role == UserRoleEnum.PROGRAM_ADMIN)
        {
          //  await repoComment.MarkConsultantCommentsAsReadAsync(request.CommentIds);
        }
        return true;
    }
}
