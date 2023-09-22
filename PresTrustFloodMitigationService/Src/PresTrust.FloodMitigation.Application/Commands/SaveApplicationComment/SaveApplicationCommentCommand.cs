namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationCommentCommand: IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string Comment { get; set; }
    public int CommentTypeId { get; set; }
}
