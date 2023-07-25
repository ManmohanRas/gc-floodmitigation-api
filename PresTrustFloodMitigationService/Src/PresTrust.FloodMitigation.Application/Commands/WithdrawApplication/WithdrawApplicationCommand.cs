namespace PresTrust.FloodMitigation.Application.Commands;

public class WithdrawApplicationCommand : IRequest<bool>
{
    public int ApplicationId { get; set; }
}
