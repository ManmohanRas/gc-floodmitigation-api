namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteApplicationCommentCommand: IRequest<bool>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string UserId { get; set; }

}
