namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetApplicationFeedbacksQuery: IRequest<IEnumerable<GetApplicationFeedbacksQueryViewModel>>
    {
        public int ApplicationId { get; set; }
    }
}
