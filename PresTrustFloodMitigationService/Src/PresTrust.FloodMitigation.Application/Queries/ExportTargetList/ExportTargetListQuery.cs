namespace PresTrust.FloodMitigation.Application.Queries;

public class ExportTargetListQuery: IRequest<Unit>
{
    public int AgencyId { get; set; }
    public string AgencyName { get; set; }
}
