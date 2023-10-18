namespace PresTrust.FloodMitigation.Application.Commands;

public class ReleaseApplicationPaymentsCommand: IRequest<bool>
{
    public int ApplicationId { get; set; }
    public IEnumerable<FloodParcelReleaseOfFundsViewModel> Payments { get; set; }
}
