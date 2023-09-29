namespace PresTrust.FloodMitigation.Application.Queries; 
public class GetPropCommentsQueryHandler : IRequestHandler<GetPropCommentsQuery, IEnumerable<GetPropCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly ICommentPropRepository repoComment;
    public GetPropCommentsQueryHandler(
        IMapper mapper,
        ICommentPropRepository repoComment)
    {
        this.repoComment = repoComment;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<GetPropCommentsQueryViewModel>> Handle(GetPropCommentsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodPropertyCommentEntity> results = default;
        results = await this.repoComment.GetCommentsAsync(request.ApplicationId, request.Pamspin);

        var comments = mapper.Map<IEnumerable<FloodPropertyCommentEntity>, IEnumerable<GetPropCommentsQueryViewModel>>(results);

        return comments;
    }
}
