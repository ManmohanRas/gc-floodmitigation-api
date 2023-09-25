namespace PresTrust.FloodMitigation.Application;

public class ApplicationDocumentTreeBuilder
{
    #region " Members ... "

    private List<ApplicationDocumentTypeViewModel> documentsTree = default;
    private IEnumerable<FloodApplicationDocumentEntity> documents = default;
    private MapperConfiguration _autoMapperConfig;


    #endregion

    #region " Procedures (Documents) ..."

    #region " ctor ..."

    public ApplicationDocumentTreeBuilder(IEnumerable<FloodApplicationDocumentEntity> documents, bool buildChecklist = false)
    {
        this.documents = documents ?? Enumerable.Empty<FloodApplicationDocumentEntity>();

        _autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FloodApplicationDocumentEntity, ApplicationDocumentViewModel>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()));
        });
        
           BuildDocuments();
    }

    #endregion

    public List<ApplicationDocumentTypeViewModel> DocumentsTree { get => documentsTree; }

    private void BuildDocuments()
    {
        if (!documents.Any())
            return;

        documentsTree = new List<ApplicationDocumentTypeViewModel>();
        var mapper = _autoMapperConfig.CreateMapper();

        // Group the document types using DocumentType as the key value 
        // and selecting list of documents for each value.
        IEnumerable<IGrouping<string, FloodApplicationDocumentEntity>> query =
            documents.GroupBy(doc => doc.DocumentType.ToString());

        // Iterate over each IGrouping in the collection.
        foreach (IGrouping<string, FloodApplicationDocumentEntity> docGroup in query)
        {
            List<ApplicationDocumentViewModel> docs = new List<ApplicationDocumentViewModel>();
            foreach (var doc in docGroup)
            {
                var vm = mapper.Map<FloodApplicationDocumentEntity, ApplicationDocumentViewModel>(doc);
                if (doc.Id > 0) docs.Add(vm);
            }

            var vmDocType = new ApplicationDocumentTypeViewModel()
            {
                DocumentType = docGroup.Key,
                Documents = docs
            };

            documentsTree.Add(vmDocType);
        }
    }

    #endregion
}
