namespace PresTrust.FloodMitigation.Application.Queries;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IEnumerable<GetCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly ICommentRepository repoComment;
    public GetCommentsQueryHandler(
          IMapper mapper,
          ICommentRepository repoComment) 
    {
        this.mapper = mapper;
        this.repoComment = repoComment;
    }

    public async Task<IEnumerable<GetCommentsQueryViewModel>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodCommentsEntity> results = default;
        if (request.IsConsultantComment)
            results = await this.repoComment.GetAllConsultantCommentsAsync(request.ApplicationId);
        else
            results = await this.repoComment.GetAllCommentsAsync(request.ApplicationId);

        var comments = mapper.Map<IEnumerable<FloodCommentsEntity>, IEnumerable<GetCommentsQueryViewModel>>(results);

        return comments;
    }
}
