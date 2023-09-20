namespace PresTrust.FloodMitigation.Application;

public class DocumentTreeBuilder
{
    #region " Members ... "

    private List<DocumentTypeViewModel> documentsTree = default;
    private IEnumerable<FloodDocumentEntity> documents = default;
    private MapperConfiguration _autoMapperConfig;

    private List<DocumentCheckListSectionViewModel> documentCheckListItems = default;


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

        if (buildChecklist == true)
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

    public List<DocumentTypeViewModel> DocumentsTree { get => documentsTree; }
    public List<DocumentCheckListSectionViewModel> DocumentCheckListItems { get => documentCheckListItems; }

    #endregion


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

    private void BuildDocumentCheckListTree()
    {
        if (!documents.Any())
            return;

        var mapper = _autoMapperConfig.CreateMapper();
        documentCheckListItems = documents.OrderBy(s => s.SectionId).Where(s => s.Id > 0).GroupBy(s => s.Section).Select(s => new DocumentCheckListSectionViewModel()
        {
            Section = SetSectionTitle(s.Key),
            DocumentCheckListDocTypeItems = s.GroupBy(d => d.DocumentType).Select(d => {
                var item = d.FirstOrDefault();
                return new DocumentCheckListDocTypeViewModel()
                {
                    Id = item.Id,
                    ApplicationId = item.ApplicationId,
                    Section = item.Section.ToString(),
                    DocumentType = item.DocumentType.ToString(),
                    Documents = d.Select(o => {
                        return mapper.Map<FloodDocumentEntity, DocumentViewModel>(o);
                    }).ToList() ?? new List<DocumentViewModel>()
                };
            }).ToList() ?? new List<DocumentCheckListDocTypeViewModel>()
        }).ToList() ?? new List<DocumentCheckListSectionViewModel>();
        if (documentCheckListItems.Count > 0)
            documentCheckListItems = documentCheckListItems.Where(o => !string.IsNullOrWhiteSpace(o.Section)).ToList();
    }

    private string SetSectionTitle(ApplicationSectionEnum enumSection)
    {
        string title = string.Empty;
        switch (enumSection)
        {
            case ApplicationSectionEnum.DECLARATION_OF_INTENT:
                title = "Declaration Of Intent";
                break;
            case ApplicationSectionEnum.ROLES:
                title = "Roles";
                break;
            case ApplicationSectionEnum.OVERVIEW:
                title = "Overview";
                break;
            case ApplicationSectionEnum.PROJECT_AREA:
                title = "Project Area";
                break;
            case ApplicationSectionEnum.OTHER_DOCUMENTS:
                title = "Other Documents";
                break;
            case ApplicationSectionEnum.FINANCE:
                title = "Finance";
                break;
            case ApplicationSectionEnum.SIGNATORY:
                title = "Signatory";
                break;
        }
        return title;
    }

    #endregion

}
