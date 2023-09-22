namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationCommentsQueryHandler : IRequestHandler<GetApplicationCommentsQuery, IEnumerable<GetApplicationCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IApplicationCommentRepository repoComment;
    public GetApplicationCommentsQueryHandler(
          IMapper mapper,
          IApplicationCommentRepository repoComment) 
    {
        this.mapper = mapper;
        this.repoComment = repoComment;
    }

    public async Task<IEnumerable<GetApplicationCommentsQueryViewModel>> Handle(GetApplicationCommentsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodApplicationCommentEntity> results = default;
     
        results = await this.repoComment.GetAllCommentsAsync(request.ApplicationId);

        var comments = mapper.Map<IEnumerable<FloodApplicationCommentEntity>, IEnumerable<GetApplicationCommentsQueryViewModel>>(results);

        return comments;
    }
}
