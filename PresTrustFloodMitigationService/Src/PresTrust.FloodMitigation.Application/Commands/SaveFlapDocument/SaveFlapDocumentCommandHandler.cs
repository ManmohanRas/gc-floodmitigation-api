using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveFlapDocumentCommandHandler: IRequestHandler<SaveFlapDocumentCommand, SaveFlapDocumentCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IFlapModuleRepository repoFlap;

    public SaveFlapDocumentCommandHandler
        (
         IMapper mapper,
         IPresTrustUserContext userContext,
         IFlapModuleRepository repoFlap
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoFlap = repoFlap;
    }

    public async Task<SaveFlapDocumentCommandViewModel> Handle(SaveFlapDocumentCommand request, CancellationToken cancellationToken)
    {
        var reqDocument = mapper.Map<SaveFlapDocumentCommand, FloodFlapDocumentEntity>(request );
        reqDocument.LastUpdatedBy = userContext.Email;

        reqDocument = await repoFlap.SaveFlapDocumentAsync(reqDocument);

        var document = mapper.Map<FloodFlapDocumentEntity, SaveFlapDocumentCommandViewModel>(reqDocument);

        return document;
    }
}
