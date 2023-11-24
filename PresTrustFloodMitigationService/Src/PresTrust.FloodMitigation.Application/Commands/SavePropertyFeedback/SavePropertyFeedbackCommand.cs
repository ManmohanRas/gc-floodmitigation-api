namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyFeedbackCommand : IRequest<int>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string Feedback { get; set; }
    public string Section { get; set; }
    public bool RequestForCorrection { get; set; }
}
