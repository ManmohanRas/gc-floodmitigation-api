namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkFeedbacksAsReadCommand : IRequest<bool>
{
    public List<int> FeedbackIds { get; set; }

}
