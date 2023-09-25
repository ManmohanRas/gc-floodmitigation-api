namespace PresTrust.FloodMitigation.Application.Commands;

public class MarkApplicationCommentsAsReadCommand:IRequest<bool>
{
    public List<int> CommentIds { get; set; }

}
