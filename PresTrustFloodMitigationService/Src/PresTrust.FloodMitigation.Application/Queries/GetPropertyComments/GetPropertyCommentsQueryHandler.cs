namespace PresTrust.FloodMitigation.Application.Queries; 
public class GetPropertyCommentsQueryHandler : IRequestHandler<GetPropertyCommentsQuery, IEnumerable<GetPropertyCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly ICommentPropRepository repoComment;
    private readonly IPresTrustUserContext userContext;
    public GetPropertyCommentsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        ICommentPropRepository repoComment)
    {
        this.repoComment = repoComment;
        this.userContext = userContext; 
        this.mapper = mapper;
    }
    public async Task<IEnumerable<GetPropertyCommentsQueryViewModel>> Handle(GetPropertyCommentsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        IEnumerable<FloodPropertyCommentEntity> results = default;
        results = await this.repoComment.GetCommentsAsync(request.ApplicationId, request.PamsPin);

        var comments = mapper.Map<IEnumerable<FloodPropertyCommentEntity>, IEnumerable<GetPropertyCommentsQueryViewModel>>(results);

        return comments;
    }
}
