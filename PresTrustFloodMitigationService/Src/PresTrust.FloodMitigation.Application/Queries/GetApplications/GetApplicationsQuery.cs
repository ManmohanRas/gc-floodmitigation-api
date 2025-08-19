namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationsQuery : IRequest<IEnumerable<GetApplicationsQueryViewModel>>
{
    public string UserId { get; set; }
}
