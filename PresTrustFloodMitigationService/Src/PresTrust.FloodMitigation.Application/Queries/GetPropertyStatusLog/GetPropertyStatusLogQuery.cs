namespace PresTrust.FloodMitigation.Application.Queries;

public class GetPropertyStatusLogQuery : IRequest<IEnumerable<GetPropertyStatusLogQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string? PamsPin { get; set; }
}
