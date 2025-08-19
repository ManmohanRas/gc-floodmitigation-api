namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDocumentCommand: IRequest<SaveFlapDocumentCommandViewModel>
{
    public int AgencyId { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string DocumentType { get; set; }
    public string UserId { get; set; }
}
