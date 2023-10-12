namespace PresTrust.FloodMitigation.Application;

    public class PropertyDocumentTreeBuilder
{
    #region " Members ... "

    private List<PropertyDocumentTypeViewModel> documentsTree = default;
    private IEnumerable<FloodPropertyDocumentEntity> documents = default;
    private MapperConfiguration _autoMapperConfig;



    #endregion

    #region " Procedures (Documents) ..."

    #region " ctor ..."

    public PropertyDocumentTreeBuilder(IEnumerable<FloodPropertyDocumentEntity> documents)
    {
        this.documents = documents ?? Enumerable.Empty<FloodPropertyDocumentEntity>();

        _autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FloodPropertyDocumentEntity, PropertyDocumentViewModel>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()));
        });
            
            BuildDocuments();
        
    }

    #endregion

    #region " Public Properties ..."

    public List<PropertyDocumentTypeViewModel> DocumentsTree { get => documentsTree; }

    #endregion
    private void BuildDocuments()
    {
        if (!documents.Any())
            return;

        documentsTree = new List<PropertyDocumentTypeViewModel>();
        var mapper = _autoMapperConfig.CreateMapper();

        // Group the document types using DocumentType as the key value 
        // and selecting list of documents for each value.
        IEnumerable<IGrouping<string, FloodPropertyDocumentEntity>> query =
            documents.GroupBy(doc => doc.DocumentType.ToString());

        // Iterate over each IGrouping in the collection.
        foreach (IGrouping<string, FloodPropertyDocumentEntity> docGroup in query)
        {
            List<PropertyDocumentViewModel> docs = new List<PropertyDocumentViewModel>();
            foreach (var doc in docGroup)
            {
                var vm = mapper.Map<FloodPropertyDocumentEntity, PropertyDocumentViewModel>(doc);
                if (doc.Id > 0) docs.Add(vm);
            }

            var vmDocType = new PropertyDocumentTypeViewModel()
            {
                DocumentType = docGroup.Key,
                Documents = docs
            };

            documentsTree.Add(vmDocType);
        }
    }

    private string SetSectionTitle(PropertySectionEnum enumSection)
    {
        string title = string.Empty;
        switch (enumSection)
        {
            case PropertySectionEnum.PROPERTY:
                title = "Property";
                break;
            case PropertySectionEnum.OTHER_DOCUMENTS:
                title = "OtherDocuments";
                break;
            case PropertySectionEnum.SOFT_COSTS:
                title = "SoftCosts";
                break;
        }
        return title;
    }

    #endregion
}


