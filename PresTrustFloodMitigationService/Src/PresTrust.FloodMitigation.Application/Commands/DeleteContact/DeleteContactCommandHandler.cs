namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IContactRepository repoContact;

    public DeleteContactCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IContactRepository repoContact)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoContact = repoContact;
    }
    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        var reqContact = mapper.Map<DeleteContactCommand, FloodContactEntity>(request);
        await repoContact.DeleteContactAsync(reqContact);
        return true;
    }
}
