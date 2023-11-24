namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyCommentCommandHandler : BaseHandler, IRequestHandler<SavePropertyCommentCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ICommentPropRepository repoComment;
   // private readonly IBrokenRuleRepository repoBrokenRules;

    public SavePropertyCommentCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ICommentPropRepository repoComment
       // IBrokenRuleRepository repoBrokenRules

    ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoComment = repoComment;
       // this.repoBrokenRules = repoBrokenRules;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SavePropertyCommentCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        AuthorizationCheck(application);

        // map command object to the HistCommentsEntity
        var reqComment = mapper.Map<SavePropertyCommentCommand, FloodPropertyCommentEntity>(request);
        reqComment.LastUpdatedBy = userContext.Email;

        // save comment
        FloodPropertyCommentEntity comment = default;

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            comment = await this.repoComment.SaveCommentsAsync(reqComment);
            scope.Complete();
        };

        return comment.Id;
    }
    /// <summary>
    /// Ensure that a user has the relevant authorizations to perform an action
    /// </summary>
    private void AuthorizationCheck(FloodApplicationEntity application)
    {
        // security
        userContext.DeriveRole(application.AgencyId);
    }
}
