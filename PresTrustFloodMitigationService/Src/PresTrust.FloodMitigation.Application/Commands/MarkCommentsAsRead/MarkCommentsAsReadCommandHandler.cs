namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkCommentsAsReadCommandHandler : IRequestHandler<MarkCommentsAsReadCommand, bool>
{
    private IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly ICommentRepository repoComment;

    public MarkCommentsAsReadCommandHandler(IMapper mapper,
        IPresTrustUserContext userContext,
        ICommentRepository repoComment
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoComment = repoComment;
    }
    public async Task<bool> Handle(MarkCommentsAsReadCommand request, CancellationToken cancellationToken)
    {
        if (userContext.Role == UserRoleEnum.PROGRAM_ADMIN)
        {
            await repoComment.MarkConsultantCommentsAsReadAsync(request.CommentIds);
        }
        return true;
    }
}
