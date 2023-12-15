namespace PresTrust.FloodMitigation.Application.Commands;

public class DeleteFlapDocumentCommandHandler: IRequestHandler<DeleteFlapDocumentCommand, bool>
{
    private IMapper mapper;
    private IFlapModuleRepository repoFlap;

    public DeleteFlapDocumentCommandHandler(
        IMapper mapper,
        IFlapModuleRepository repoFlap)
    {
        this.mapper = mapper;
        this.repoFlap = repoFlap;
    }

    public async Task<bool> Handle(DeleteFlapDocumentCommand request, CancellationToken cancellationToken)
    {
          //delete flap document
          await repoFlap.DeleteFlapDocumentAsync(request.Id);

        return true;
    }
}
