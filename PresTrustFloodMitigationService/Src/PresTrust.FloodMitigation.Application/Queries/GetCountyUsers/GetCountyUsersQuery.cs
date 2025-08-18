namespace PresTrust.FloodMitigation.Application.Queries;

public class GetCountyUsersQuery : IRequest<IEnumerable<PresTrustUserEntity>>
{
    public string UserId { get; set; }
}
