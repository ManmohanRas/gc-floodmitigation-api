namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalCommentsQueryHandler : IRequestHandler<GetMunicipalCommentsQuery, IEnumerable<GetMunicipalCommentsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IMunicipalCommentRepository repoComment;
    public GetMunicipalCommentsQueryHandler(
          IMapper mapper,
          IPresTrustUserContext userContext,
          IMunicipalCommentRepository repoComment)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoComment = repoComment;
    }

    public async Task<IEnumerable<GetMunicipalCommentsQueryViewModel>> Handle(GetMunicipalCommentsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        IEnumerable<FloodMunicipalCommentEntity> results = default;

        results = await this.repoComment.GetAllCommentsAsync(request.AgencyId);

        var comments = mapper.Map<IEnumerable<FloodMunicipalCommentEntity>, IEnumerable<GetMunicipalCommentsQueryViewModel>>(results);

        return comments;
    }
}
