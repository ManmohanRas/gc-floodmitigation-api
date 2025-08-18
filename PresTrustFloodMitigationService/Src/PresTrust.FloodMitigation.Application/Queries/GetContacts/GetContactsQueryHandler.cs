namespace PresTrust.FloodMitigation.Application.Queries;

public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery,IEnumerable<GetContactsQueryViewModel>>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IContactRepository repoContact;

    public GetContactsQueryHandler(
          IMapper mapper,
          IPresTrustUserContext userContext,
          IContactRepository repoContact)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoContact = repoContact;
    }
    public async Task<IEnumerable<GetContactsQueryViewModel>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        IEnumerable<FloodContactEntity> results = default;

        results = await this.repoContact.GetAllContactsAsync(request.ApplicationId);

        var contacts = mapper.Map<IEnumerable<FloodContactEntity>, IEnumerable<GetContactsQueryViewModel>>(results);

        return contacts;
    }
}
