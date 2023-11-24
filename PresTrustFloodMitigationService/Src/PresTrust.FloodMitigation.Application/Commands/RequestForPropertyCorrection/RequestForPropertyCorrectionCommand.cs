namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class represents api's command input model and returns the response object
/// </summary>
public class RequestForPropertyCorrectionCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
