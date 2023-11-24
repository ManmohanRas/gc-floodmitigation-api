namespace PresTrust.FloodMitigation.Application.Commands;

public class DeletePropertyFeedbackCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
}
