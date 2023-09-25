namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkApplicationFeedbacksAsReadCommand : IRequest<bool>
{
    public List<int> FeedbackIds { get; set; }

}
