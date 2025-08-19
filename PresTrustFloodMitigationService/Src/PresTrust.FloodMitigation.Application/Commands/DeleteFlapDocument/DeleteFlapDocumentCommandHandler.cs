namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFlapDocumentCommandHandler: IRequestHandler<DeleteFlapDocumentCommand, bool>
{
    private IMapper mapper;
    private IPresTrustUserContext userContext;
    private IFlapModuleRepository repoFlap;

    public DeleteFlapDocumentCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IFlapModuleRepository repoFlap)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFlap = repoFlap;
    }

    public async Task<bool> Handle(DeleteFlapDocumentCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        //delete flap document
        await repoFlap.DeleteFlapDocumentAsync(request.Id);

        return true;
    }
}
