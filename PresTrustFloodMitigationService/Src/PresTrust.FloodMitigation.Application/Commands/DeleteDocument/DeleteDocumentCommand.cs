namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class DeleteDocumentCommand: IRequest<bool>
{
    public int ApplicationId { get; set; }
    public int Id { get; set; }
}
