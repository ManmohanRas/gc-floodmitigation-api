namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropCommentCommand : IRequest<bool>
{

    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string? Pamspin { get; set; }
}
