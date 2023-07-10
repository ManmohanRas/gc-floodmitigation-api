namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveDocumentDetailsCommandViewModel
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public string DocumentType { get; set; }
}
