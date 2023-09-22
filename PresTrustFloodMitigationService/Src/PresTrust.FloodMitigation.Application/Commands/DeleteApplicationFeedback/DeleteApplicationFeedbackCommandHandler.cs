namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationFeedbackCommandHandler : IRequestHandler<DeleteApplicationFeedbackCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IApplicationFeedbackRepository repoFeedback;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public DeleteApplicationFeedbackCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationFeedbackRepository repoFeedback
    ) 
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoFeedback = repoFeedback;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(DeleteApplicationFeedbackCommand request, CancellationToken cancellationToken)
    {
        // get application details
       // var application = await GetIfApplicationExists(request.ApplicationId);

        // map command object to the FloodApplicationFeedbackEntity
        var reqFeedback = mapper.Map<DeleteApplicationFeedbackCommand, FloodApplicationFeedbackEntity>(request);

        // delete feedback
        await repoFeedback.DeleteAsync(reqFeedback);
        return true;
    }

}
