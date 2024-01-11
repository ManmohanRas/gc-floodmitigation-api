namespace PresTrust.FloodMitigation.Application.Commands;

public class ImportTargetListCommand: IRequest<Unit>
{
    public int AgencyId { get; set; }

}
