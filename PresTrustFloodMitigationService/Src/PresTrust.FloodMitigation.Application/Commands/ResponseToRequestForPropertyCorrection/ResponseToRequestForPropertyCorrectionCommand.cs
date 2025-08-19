namespace PresTrust.FloodMitigation.Application.Commands;


/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class ResponseToRequestForPropertyCorrectionCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
    public List<string> Sections { get; set; }
    public string Feedback { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }
}
