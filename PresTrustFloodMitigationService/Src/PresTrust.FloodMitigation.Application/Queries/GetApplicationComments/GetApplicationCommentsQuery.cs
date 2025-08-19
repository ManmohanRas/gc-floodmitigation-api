namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationCommentsQuery : IRequest<IEnumerable<GetApplicationCommentsQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
