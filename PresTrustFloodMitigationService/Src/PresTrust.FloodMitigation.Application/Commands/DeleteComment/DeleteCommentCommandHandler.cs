namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IMapper mapper;
    private readonly ICommentRepository repoComment;


    public DeleteCommentCommandHandler(
        IMapper mapper,
        ICommentRepository repoComment)
    {
        this.mapper = mapper;
        this.repoComment = repoComment;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        // map command object to the HistCommentsEntity
        var reqComment = mapper.Map<DeleteCommentCommand, FloodCommentsEntity>(request);

        // delete comment
        await repoComment.DeleteCommentAsync(reqComment);

        return true;
    }
}
