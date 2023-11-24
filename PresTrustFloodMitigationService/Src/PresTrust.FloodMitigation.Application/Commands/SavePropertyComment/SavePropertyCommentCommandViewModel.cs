namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyCommentCommandViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string PamsPin { get; set; }
    public string Comment { get; set; } = "";
}
