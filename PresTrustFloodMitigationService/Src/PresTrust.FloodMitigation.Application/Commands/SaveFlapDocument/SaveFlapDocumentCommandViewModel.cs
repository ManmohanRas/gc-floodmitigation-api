namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDocumentCommandViewModel
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string DocumentType { get; set; }
}
