namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyFeedbackCommandHandler : IRequestHandler<SavePropertyFeedbackCommand, int>
{

    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackPropRepository repoFeedback;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public SavePropertyFeedbackCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IFeedbackPropRepository repoFeedback
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
    public async Task<int> Handle(SavePropertyFeedbackCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // get application details
        //var application = await GetIfApplicationExists(request.ApplicationId);

        var feedback = mapper.Map<SavePropertyFeedbackCommand, FloodPropertyFeedbackEntity>(request);
        feedback.LastUpdatedBy = userContext.Email;
        feedback.CorrectionStatus = feedback.Section == PropertySectionEnum.NONE ? PropertyCorrectionStatusEnum.NONE.ToString() : PropertyCorrectionStatusEnum.PENDING.ToString();
        feedback = await repoFeedback.SavePropertyFeedbackAsync(feedback);
        return feedback.Id;
    }
}
