namespace PresTrust.FloodMitigation.Application.Queries;

public class GetFlapTargetAreasQuery: IRequest<IEnumerable<GetFlapTargetAreasQueryViewModel>>
{
    public int Id { get; set; }
    public int AgencyId { get; set; }
}
