namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class DeleteApplicationDocumentCommand: IRequest<bool>
{
    public int ApplicationId { get; set; }
    public int Id { get; set; }
    public string UserId { get; set; }
}
