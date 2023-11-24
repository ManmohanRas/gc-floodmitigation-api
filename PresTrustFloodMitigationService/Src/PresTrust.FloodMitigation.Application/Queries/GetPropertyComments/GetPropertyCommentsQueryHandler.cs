namespace PresTrust.FloodMitigation.Application.Queries; 
public class GetPropertyCommentsQueryHandler : IRequestHandler<GetPropertyCommentsQuery, IEnumerable<GetPropertyCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly ICommentPropRepository repoComment;
    public GetPropertyCommentsQueryHandler(
        IMapper mapper,
        ICommentPropRepository repoComment)
    {
        this.repoComment = repoComment;
        this.mapper = mapper;
    }
    public async Task<IEnumerable<GetPropertyCommentsQueryViewModel>> Handle(GetPropertyCommentsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodPropertyCommentEntity> results = default;
        results = await this.repoComment.GetCommentsAsync(request.ApplicationId, request.PamsPin);

        var comments = mapper.Map<IEnumerable<FloodPropertyCommentEntity>, IEnumerable<GetPropertyCommentsQueryViewModel>>(results);

        return comments;
    }
}
