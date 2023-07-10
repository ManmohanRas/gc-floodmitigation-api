namespace PresTrust.FloodMitigation.Application;

public class DocumentTreeBuilder
{
    #region " Members ... "

    private List<DocumentTypeViewModel> documentsTree = default;
    private IEnumerable<FloodDocumentEntity> documents = default;
    private MapperConfiguration _autoMapperConfig;


    #endregion

    #region " Procedures (Documents) ..."

    #region " ctor ..."

    public DocumentTreeBuilder(IEnumerable<FloodDocumentEntity> documents, bool buildChecklist = false)
    {
        this.documents = documents ?? Enumerable.Empty<FloodDocumentEntity>();

        _autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FloodDocumentEntity, DocumentViewModel>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()));
        });
        
           BuildDocuments();
    }

    #endregion

    public List<DocumentTypeViewModel> DocumentsTree { get => documentsTree; }

    private void BuildDocuments()
    {
        if (!documents.Any())
            return;

        documentsTree = new List<DocumentTypeViewModel>();
        var mapper = _autoMapperConfig.CreateMapper();

        // Group the document types using DocumentType as the key value 
        // and selecting list of documents for each value.
        IEnumerable<IGrouping<string, FloodDocumentEntity>> query =
            documents.GroupBy(doc => doc.DocumentType.ToString());

        // Iterate over each IGrouping in the collection.
        foreach (IGrouping<string, FloodDocumentEntity> docGroup in query)
        {
            List<DocumentViewModel> docs = new List<DocumentViewModel>();
            foreach (var doc in docGroup)
            {
                var vm = mapper.Map<FloodDocumentEntity, DocumentViewModel>(doc);
                if (doc.Id > 0) docs.Add(vm);
            }

            var vmDocType = new DocumentTypeViewModel()
            {
                DocumentType = docGroup.Key,
                Documents = docs
            };

            documentsTree.Add(vmDocType);
        }
    }

    #endregion
}
