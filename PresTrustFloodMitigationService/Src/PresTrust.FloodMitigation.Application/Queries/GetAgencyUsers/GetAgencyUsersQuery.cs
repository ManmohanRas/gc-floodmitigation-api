namespace PresTrust.FloodMitigation.Application.Queries
{
    public class GetAgencyUsersQuery: IRequest<IEnumerable<PresTrustUserEntity>>
        {
            public int AgencyId { get; set; }
        }
}
