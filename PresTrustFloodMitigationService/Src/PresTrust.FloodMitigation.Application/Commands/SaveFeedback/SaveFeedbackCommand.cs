namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFeedbackCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string Feedback { get; set; }
    public string Section { get; set; }
    public bool RequestForCorrection { get; set; }
}
