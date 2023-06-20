namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFeedbacksQueryHandler : IRequestHandler<GetFeedbacksQuery, IEnumerable<GetFeedbacksQueryViewModel>>
{
    private readonly IMapper mapper;
    //private readonly IPresTrustUserContext userContext;
    //private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IFeedbackRepository repoFeedback;

    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public GetFeedbacksQueryHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        //IOptions<SystemParameterConfiguration> systemParamOptions,
        IFeedbackRepository repoFeedback
    )
    {
        this.mapper = mapper;
        //this.userContext = userContext;
        //this.systemParamOptions = systemParamOptions.Value;
        this.repoFeedback = repoFeedback;
    }

    public async Task<IEnumerable<GetFeedbacksQueryViewModel>> Handle(GetFeedbacksQuery request, CancellationToken cancellationToken)
    {
        // get feedbacks for a given application id
        var feedbacks = await repoFeedback.GetFeedbacksAsync(request.ApplicationId);

        // map command object to the GetFeedbacksQueryViewModel
        var result = mapper.Map<IEnumerable<FloodFeedbackEntity>, IEnumerable<GetFeedbacksQueryViewModel>>(feedbacks);
        return result;
    }
}
