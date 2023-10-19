namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleasePaymentsCommand: IRequest<bool>
{
    public int ApplicationId { get; set; }
    public IEnumerable<FloodParcelReleaseOfFundsViewModel> Payments { get; set; }
}
