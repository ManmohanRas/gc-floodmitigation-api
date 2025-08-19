namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationFeedbacksQueryHandler : IRequestHandler<GetApplicationFeedbacksQuery, IEnumerable<GetApplicationFeedbacksQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    //private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationFeedbackRepository repoFeedback;

    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public GetApplicationFeedbacksQueryHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        //IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationFeedbackRepository repoFeedback
    )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        //this.systemParamOptions = systemParamOptions.Value;
        this.repoFeedback = repoFeedback;
    }

    public async Task<IEnumerable<GetApplicationFeedbacksQueryViewModel>> Handle(GetApplicationFeedbacksQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        // get feedbacks for a given application id
        var feedbacks = await repoFeedback.GetFeedbacksAsync(request.ApplicationId);

        // map command object to the GetFeedbacksQueryViewModel
        var result = mapper.Map<IEnumerable<FloodApplicationFeedbackEntity>, IEnumerable<GetApplicationFeedbacksQueryViewModel>>(feedbacks);
        return result;
    }
}
