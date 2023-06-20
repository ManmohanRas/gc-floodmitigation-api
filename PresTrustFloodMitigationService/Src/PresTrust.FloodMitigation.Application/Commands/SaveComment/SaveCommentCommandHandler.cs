namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveCommentCommandHandler : IRequestHandler<SaveCommentCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ICommentRepository repoComment;

    public SaveCommentCommandHandler
        (
         IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ICommentRepository repoComment
        ) 
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoComment = repoComment;
    }

    public async Task<int> Handle(SaveCommentCommand request, CancellationToken cancellationToken)
    {
        
        var reqComment = mapper.Map<SaveCommentCommand, FloodCommentsEntity>(request);
        reqComment.LastUpdatedBy = userContext.Email;

        // save comment
        FloodCommentsEntity comment = default;

        if (reqComment.IsConsultantComment)
            comment = await this.repoComment.SaveConsultantCommentAsync(reqComment);
        else
            comment = await this.repoComment.SaveAsync(reqComment);

        return comment.Id;
    }
}
