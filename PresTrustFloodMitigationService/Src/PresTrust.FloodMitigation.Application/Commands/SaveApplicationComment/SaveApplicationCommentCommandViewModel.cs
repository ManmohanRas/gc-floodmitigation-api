namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveApplicationCommentCommandViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string Comment { get; set; } = "";
}
