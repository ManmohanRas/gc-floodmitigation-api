namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkCommentsAsReadCommand:IRequest<bool>
{
    public List<int> CommentIds { get; set; }

}
