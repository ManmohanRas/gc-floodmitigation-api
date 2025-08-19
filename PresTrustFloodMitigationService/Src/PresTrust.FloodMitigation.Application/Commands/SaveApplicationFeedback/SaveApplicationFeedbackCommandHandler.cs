namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationFeedbackCommandHandler : IRequestHandler<SaveApplicationFeedbackCommand, int>
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
    public SaveApplicationFeedbackCommandHandler
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
    public async Task<int> Handle(SaveApplicationFeedbackCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get application details
        //var application = await GetIfApplicationExists(request.ApplicationId);

        var feedback = mapper.Map<SaveApplicationFeedbackCommand, FloodApplicationFeedbackEntity>(request);
        feedback.LastUpdatedBy = userContext.Email;
        feedback.CorrectionStatus = feedback.Section == ApplicationSectionEnum.NONE ? ApplicationCorrectionStatusEnum.NONE.ToString() : ApplicationCorrectionStatusEnum.PENDING.ToString();
        feedback = await repoFeedback.SaveAsync(feedback);
        return feedback.Id;
    }
}
