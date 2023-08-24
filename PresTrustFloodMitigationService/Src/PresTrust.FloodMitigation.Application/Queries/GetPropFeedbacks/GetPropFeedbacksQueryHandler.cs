namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropFeedbacksQueryHandler: IRequestHandler<GetPropFeedbacksQuery, IEnumerable<GetPropFeedbacksQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IFeedbackPropRepository repoFeedback;

    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userContext"></param>
    /// <param name="systemParamOptions"></param>
    /// <param name="repoApplication"></param>
    /// <param name="repoFeedback"></param>
    public GetPropFeedbacksQueryHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IFeedbackPropRepository repoFeedback
    )
    {
        this.mapper = mapper;
        this.repoFeedback = repoFeedback;
    }

    public async Task<IEnumerable<GetPropFeedbacksQueryViewModel>> Handle(GetPropFeedbacksQuery request, CancellationToken cancellationToken)
    {
        // get feedbacks for a given application id
        var feedbacks = await repoFeedback.GetPropFeedbackAsync(request.ApplicationId, request.Pamspin);

        // map command object to the GetPropFeedbacksQueryViewModel
        var result = mapper.Map<IEnumerable<FloodPropFeedbackEntity>, IEnumerable<GetPropFeedbacksQueryViewModel>>(feedbacks);
        return result;
    }
}
