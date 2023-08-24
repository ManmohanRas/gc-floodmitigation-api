namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropCommentCommandHandler : BaseHandler, IRequestHandler<SavePropCommentCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ICommentPropRepository repoComment;
   // private readonly IBrokenRuleRepository repoBrokenRules;

    public SavePropCommentCommandHandler
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
    public async Task<int> Handle(SavePropCommentCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        AuthorizationCheck(application);

        // map command object to the HistCommentsEntity
        var reqComment = mapper.Map<SavePropCommentCommand, FloodPropCommentEntity>(request);
        reqComment.LastUpdatedBy = userContext.Email;

        // save comment
        FloodPropCommentEntity comment = default;

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
