namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalCommentsQueryHandler : IRequestHandler<GetMunicipalCommentsQuery, IEnumerable<GetMunicipalCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IMunicipalCommentRepository repoComment;
    public GetMunicipalCommentsQueryHandler(
          IMapper mapper,
          IMunicipalCommentRepository repoComment)
    {
        this.mapper = mapper;
        this.repoComment = repoComment;
    }

    public async Task<IEnumerable<GetMunicipalCommentsQueryViewModel>> Handle(GetMunicipalCommentsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<FloodMunicipalCommentEntity> results = default;

        results = await this.repoComment.GetAllCommentsAsync(request.AgencyId);

        var comments = mapper.Map<IEnumerable<FloodMunicipalCommentEntity>, IEnumerable<GetMunicipalCommentsQueryViewModel>>(results);

        return comments;
    }
}
