namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class SaveApplicationDocumentCommand: IRequest<SaveApplicationDocumentCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public string DocumentType { get; set; }
    public int? otherFundingSource { get; set; }
}
