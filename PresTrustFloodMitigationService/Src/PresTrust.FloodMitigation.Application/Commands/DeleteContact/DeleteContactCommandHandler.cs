namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IContactRepository repoContact;

    public DeleteContactCommandHandler(
        IMapper mapper,
        IContactRepository repoContact)
    {
        this.mapper = mapper;
        this.repoContact = repoContact;
    }
    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var reqContact = mapper.Map<DeleteContactCommand, FloodContactEntity>(request);
        await repoContact.DeleteContactAsync(reqContact);
        return true;
    }
}
