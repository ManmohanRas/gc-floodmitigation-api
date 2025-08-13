namespace PresTrust.FloodMitigation.Application.Commands;

public class ApprovePropertyCommand : IRequest<ApprovePropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public string PamsPin { get; set; }
    public string UserId { get; set; }
}
