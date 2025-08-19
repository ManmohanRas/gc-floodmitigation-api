namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationCommentsQueryHandler : IRequestHandler<GetApplicationCommentsQuery, IEnumerable<GetApplicationCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationCommentRepository repoComment;
    private IPresTrustUserContext userContext;
    public GetApplicationCommentsQueryHandler(
          IMapper mapper,
          IPresTrustUserContext userContext,
          IApplicationCommentRepository repoComment) 
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoComment = repoComment;
    }

    public async Task<IEnumerable<GetApplicationCommentsQueryViewModel>> Handle(GetApplicationCommentsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        IEnumerable<FloodApplicationCommentEntity> results = default;
     
        results = await this.repoComment.GetAllCommentsAsync(request.ApplicationId);

        var comments = mapper.Map<IEnumerable<FloodApplicationCommentEntity>, IEnumerable<GetApplicationCommentsQueryViewModel>>(results);

        return comments;
    }
}
