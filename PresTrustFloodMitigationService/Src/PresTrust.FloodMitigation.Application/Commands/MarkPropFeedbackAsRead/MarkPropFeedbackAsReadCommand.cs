namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkPropFeedbackAsReadCommand : IRequest<bool>
{
    public List<int> FeedbackIds { get; set; }
}
