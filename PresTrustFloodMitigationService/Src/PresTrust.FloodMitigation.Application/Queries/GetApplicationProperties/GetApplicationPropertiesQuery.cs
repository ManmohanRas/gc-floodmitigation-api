namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationPropertiesQuery : IRequest<IEnumerable<GetApplicationPropertiesQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
