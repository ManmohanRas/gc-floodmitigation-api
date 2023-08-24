namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropCommentCommandViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string? Pamspin { get; set; }
    public string Comment { get; set; } = "";
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; } = DateTime.MinValue;
}
