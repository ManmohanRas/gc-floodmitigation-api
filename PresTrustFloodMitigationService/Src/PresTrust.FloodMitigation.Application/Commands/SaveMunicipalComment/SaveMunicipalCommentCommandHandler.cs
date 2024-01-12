namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveMunicipalCommentCommandHandler : IRequestHandler<SaveMunicipalCommentCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IMunicipalCommentRepository repoComment;

    public SaveMunicipalCommentCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IMunicipalCommentRepository repoComment
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoComment = repoComment;
    }

    public async Task<int> Handle(SaveMunicipalCommentCommand request, CancellationToken cancellationToken)
    {

        var reqComment = mapper.Map<SaveMunicipalCommentCommand, FloodMunicipalCommentEntity>(request);


        // save comment
        FloodMunicipalCommentEntity comment = default;
        comment = await this.repoComment.SaveAsync(reqComment);

        return comment.Id;
    }
}

