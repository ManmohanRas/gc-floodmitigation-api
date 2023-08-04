namespace PresTrust.FloodMitigation.Application.Commands;

public class WithdrawApplicationCommand : IRequest<Unit>
{
    public int ApplicationId { get; set; }
}
