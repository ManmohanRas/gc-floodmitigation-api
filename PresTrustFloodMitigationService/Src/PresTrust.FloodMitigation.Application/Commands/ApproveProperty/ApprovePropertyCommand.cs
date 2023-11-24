namespace PresTrust.FloodMitigation.Application.Commands;

public class ApprovePropertyCommand : IRequest<ApprovePropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string PamsPin { get; set; }
}
