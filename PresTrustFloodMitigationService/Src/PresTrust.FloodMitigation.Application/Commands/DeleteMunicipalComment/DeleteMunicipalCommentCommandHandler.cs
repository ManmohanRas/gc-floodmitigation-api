namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteMunicipalCommentCommandHandler : IRequestHandler<DeleteMunicipalCommentCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IMunicipalCommentRepository repoComment;


    public DeleteMunicipalCommentCommandHandler(
        IMapper mapper,
        IMunicipalCommentRepository repoComment)
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
    public async Task<bool> Handle(DeleteMunicipalCommentCommand request, CancellationToken cancellationToken)
    {
        var reqComment = mapper.Map<DeleteMunicipalCommentCommand, FloodMunicipalCommentEntity>(request);
        await repoComment.DeleteCommentAsync(reqComment);
        return true;
    }
}

