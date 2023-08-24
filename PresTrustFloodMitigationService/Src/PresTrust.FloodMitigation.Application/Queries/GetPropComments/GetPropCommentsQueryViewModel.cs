namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropCommentsQueryViewModel
{
    public int Id { get; set; } = 0;
    public int ApplicationId { get; set; } = 0;
    public string Comment { get; set; } = "";
    public int CommentTypeId { get; set; } = 0;
    public bool MarkRead { get; set; } = false;
    public string LastUpdatedBy { get; set; } = "";
    public DateTime LastUpdatedOn { get; set; } = DateTime.MinValue;
}
