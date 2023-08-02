using DevExpress.XtraPrinting.Native;
using DevExpress.XtraRichEdit.Commands;
using static Dapper.SqlMapper;

namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class SaveDocumentDetailsCommandHandler : IRequestHandler<SaveDocumentDetailsCommand, SaveDocumentDetailsCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IDocumentRepository repoDocument;

    public SaveDocumentDetailsCommandHandler
        (
         IMapper mapper, 
         IPresTrustUserContext userContext,
         IDocumentRepository repoDocument
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoDocument = repoDocument;
    }
    public async Task<SaveDocumentDetailsCommandViewModel> Handle(SaveDocumentDetailsCommand request, CancellationToken cancellationToken)
    {
        // map command object to the HistDocumentEntity
        var reqDocument = mapper.Map<SaveDocumentDetailsCommand, FloodDocumentEntity>(request);
        reqDocument.LastUpdatedBy = userContext.Email;

        var entityDocument = await repoDocument.SaveDocumentDetailsAsync(reqDocument);

        // map entity object to the SaveDocumentCommandViewModel
        var Document = mapper.Map<FloodDocumentEntity, SaveDocumentDetailsCommandViewModel>(entityDocument);

        return Document;
    }
}
