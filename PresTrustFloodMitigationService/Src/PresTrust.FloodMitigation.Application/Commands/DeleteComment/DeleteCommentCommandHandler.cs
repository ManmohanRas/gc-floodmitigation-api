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
        var reqComment = mapper.Map<DeleteCommentCommand, FloodCommentEntity>(request);
        await repoComment.DeleteCommentAsync(reqComment);
        return true;
    }
}
