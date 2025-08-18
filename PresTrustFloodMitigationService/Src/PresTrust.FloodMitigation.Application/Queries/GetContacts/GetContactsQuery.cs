namespace PresTrust.FloodMitigation.Application.Queries;

public class GetContactsQuery : IRequest<IEnumerable<GetContactsQueryViewModel>>
{
    public int ApplicationId { get; set; }
    public string UserId { get; set; }
}
