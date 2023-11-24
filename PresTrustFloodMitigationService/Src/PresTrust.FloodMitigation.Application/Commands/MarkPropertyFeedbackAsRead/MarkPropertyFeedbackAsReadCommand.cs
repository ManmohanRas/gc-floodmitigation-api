namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkPropertyFeedbackAsReadCommand : IRequest<bool>
{
    public List<int> FeedbackIds { get; set; }
}
