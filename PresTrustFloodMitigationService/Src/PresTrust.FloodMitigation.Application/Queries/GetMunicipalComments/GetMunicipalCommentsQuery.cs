namespace PresTrust.FloodMitigation.Application.Queries;

public class GetMunicipalCommentsQuery : IRequest<IEnumerable<GetMunicipalCommentsQueryViewModel>>
{
    public int AgencyId { get; set; }
}
