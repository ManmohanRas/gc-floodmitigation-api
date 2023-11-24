namespace PresTrust.FloodMitigation.Application.Commands;

public class WithdrawPropertyCommand : IRequest<WithdrawPropertyCommandViewModel>
{
    public int ApplicationId { get; set; }
    public required string PamsPin { get; set; }
}
