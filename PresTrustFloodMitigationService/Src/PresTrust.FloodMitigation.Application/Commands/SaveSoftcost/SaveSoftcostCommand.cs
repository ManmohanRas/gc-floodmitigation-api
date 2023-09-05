namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftcostCommand : IRequest<Unit>
{
    public IEnumerable<FloodParcelSoftcostViewModel>? SoftcostLineItems { get; set; }
    
}
