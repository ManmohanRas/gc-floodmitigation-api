using DevExpress.XtraPrinting.Native;
using DevExpress.XtraRichEdit.Commands;
using static Dapper.SqlMapper;

namespace PresTrust.FloodMitigation.Application.Commands;
/// <summary>
/// This class handles the command to update data and build response
/// </summary>
public class SaveApplicationDocumentCommandHandler : IRequestHandler<SaveApplicationDocumentCommand, SaveApplicationDocumentCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly IApplicationDocumentRepository repoDocument;

    public SaveApplicationDocumentCommandHandler
        (
         IMapper mapper, 
         IPresTrustUserContext userContext,
         IApplicationDocumentRepository repoDocument
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoDocument = repoDocument;
    }
    public async Task<SaveApplicationDocumentCommandViewModel> Handle(SaveApplicationDocumentCommand request, CancellationToken cancellationToken)
    {
        // map command object to the HistDocumentEntity
        var reqDocument = mapper.Map<SaveApplicationDocumentCommand, FloodApplicationDocumentEntity>(request);
        reqDocument.LastUpdatedBy = userContext.Email;

        var entityDocument = await repoDocument.SaveApplicationDocumentDetailsAsync(reqDocument);

        // map entity object to the SaveDocumentCommandViewModel
        var Document = mapper.Map<FloodApplicationDocumentEntity, SaveApplicationDocumentCommandViewModel>(entityDocument);

        return Document;
    }
}
