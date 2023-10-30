namespace PresTrust.FloodMitigation.Application;

    public class PropertyDocumentTreeBuilder
{
    #region " Members ... "

    private List<PropertyDocumentTypeViewModel> documentsTree = default;
    private IEnumerable<FloodPropertyDocumentEntity> documents = default;
    private MapperConfiguration _autoMapperConfig;

    public List<PropertyDocumentCheckListSectionViewModel> documentCheckListItems = default;


    #endregion

    #region " Procedures (Documents) ..."

    #region " ctor ..."

    public PropertyDocumentTreeBuilder(IEnumerable<FloodPropertyDocumentEntity> documents, bool buildPropChecklist = false)
    {
        this.documents = documents ?? Enumerable.Empty<FloodPropertyDocumentEntity>();

        _autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FloodPropertyDocumentEntity, PropertyDocumentViewModel>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType.ToString()));
        });

        if (buildPropChecklist == true)
        {
            BuildDocumentCheckListTree();
        }
        else
        {
            BuildDocuments();
        }

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

    private void BuildDocumentCheckListTree()
    {
        if (!documents.Any())
            return;

        var mapper = _autoMapperConfig.CreateMapper();
        documentCheckListItems = documents.OrderBy(s => s.SectionId).Where(s => s.Id > 0).GroupBy(s => s.Section).Select(s => new PropertyDocumentCheckListSectionViewModel()
        {
            Section = SetSectionTitle(s.Key),
            PropertyDocumentCheckListDocTypeItems = s.GroupBy(d => d.DocumentType).Select(d => {
                var item = d.FirstOrDefault();
                return new PropertyDocumentChecklistDocTypeViewModel()
                {
                    Id = item.Id,
                    ApplicationId = item.ApplicationId,
                    PamsPin = item.PamsPin.ToString(),
                    Section = item.Section.ToString(),
                    DocumentType = item.DocumentType.ToString(),
                    Documents = d.Select(o => {
                        return mapper.Map<FloodPropertyDocumentEntity, PropertyDocumentViewModel>(o);
                    }).ToList() ?? new List<PropertyDocumentViewModel>()
                };
            }).ToList() ?? new List<PropertyDocumentChecklistDocTypeViewModel>()
        }).ToList() ?? new List<PropertyDocumentCheckListSectionViewModel>();
        if (documentCheckListItems.Count > 0)
            documentCheckListItems = documentCheckListItems.Where(o => !string.IsNullOrWhiteSpace(o.Section)).ToList();
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
            case PropertySectionEnum.ADMIN_DETAILS:
                title = "AdminDetails";
                break;
            case PropertySectionEnum.ADMIN_DOCUMENT_CHECKLIST:
                title = "AdminDocumentCheckList";
                break;
        }
        return title;
    }

    #endregion
}


