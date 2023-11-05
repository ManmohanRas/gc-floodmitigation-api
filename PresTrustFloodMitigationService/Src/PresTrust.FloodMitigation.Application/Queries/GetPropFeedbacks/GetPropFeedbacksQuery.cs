namespace PresTrust.FloodMitigation.Application.Queries;
public class GetPropFeedbacksQuery : IRequest<IEnumerable<GetPropFeedbacksQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
