namespace PresTrust.FloodMitigation.Application.Queries;
public class GetAllEmailTemplatesQueryViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? TemplateCode { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
