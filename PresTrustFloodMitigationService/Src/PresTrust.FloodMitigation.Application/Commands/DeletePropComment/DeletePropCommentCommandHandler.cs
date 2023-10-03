namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropCommentCommandHandler : IRequestHandler<DeletePropCommentCommand, bool>
{
    private readonly IMapper mapper;
    private readonly ICommentPropRepository repoComment;


    public DeletePropCommentCommandHandler(
        IMapper mapper,
        ICommentPropRepository repoComment)
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
    public async Task<bool> Handle(DeletePropCommentCommand request, CancellationToken cancellationToken)
    {
        // map command object to the HistCommentsEntity
        var reqComment = mapper.Map<DeletePropCommentCommand, FloodPropertyCommentEntity>(request);

        // delete comment
        await repoComment.DeleteCommentAsync(reqComment);

        return true;
    }
}
