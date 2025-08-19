namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationCommentCommandHandler : IRequestHandler<SaveApplicationCommentCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationCommentRepository repoComment;

    public SaveApplicationCommentCommandHandler
        (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationCommentRepository repoComment
        ) 
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoComment = repoComment;
    }

    public async Task<int> Handle(SaveApplicationCommentCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        var reqComment = mapper.Map<SaveApplicationCommentCommand, FloodApplicationCommentEntity>(request);
        
        // save comment
        FloodApplicationCommentEntity comment = default;
        comment = await this.repoComment.SaveAsync(reqComment);

        return comment.Id;
    }
}
