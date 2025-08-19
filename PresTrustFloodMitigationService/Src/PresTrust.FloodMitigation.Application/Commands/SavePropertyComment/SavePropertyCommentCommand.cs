namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyCommentCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
    public string PamsPin { get; set; }
    public string? Comment { get; set; }
    public int CommentTypeId { get; set; }
}
