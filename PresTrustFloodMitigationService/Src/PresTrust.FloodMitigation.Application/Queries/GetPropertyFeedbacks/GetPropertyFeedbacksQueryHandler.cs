namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyFeedbacksQueryHandler: IRequestHandler<GetPropertyFeedbacksQuery, IEnumerable<GetPropertyFeedbacksQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IFeedbackPropRepository repoFeedback;

    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public GetPropertyFeedbacksQueryHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IFeedbackPropRepository repoFeedback
    )
    {
        this.mapper = mapper;
        this.repoFeedback = repoFeedback;
    }

    public async Task<IEnumerable<GetPropertyFeedbacksQueryViewModel>> Handle(GetPropertyFeedbacksQuery request, CancellationToken cancellationToken)
    {
        // get feedbacks for a given application id
        var feedbacks = await repoFeedback.GetPropertyFeedbackAsync(request.ApplicationId, request.PamsPin);

        // map command object to the GetPropertyFeedbacksQueryViewModel
        var result = mapper.Map<IEnumerable<FloodPropertyFeedbackEntity>, IEnumerable<GetPropertyFeedbacksQueryViewModel>>(feedbacks);
        return result;
    }
}
