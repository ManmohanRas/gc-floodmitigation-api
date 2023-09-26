namespace PresTrust.FloodMitigation.Application;

public class ApplicationDocumentTreeBuilder
{
    #region " Members ... "

    private List<ApplicationDocumentTypeViewModel> documentsTree = default;
    private IEnumerable<FloodApplicationDocumentEntity> documents = default;
    private MapperConfiguration _autoMapperConfig;

    private List<DocumentCheckListSectionViewModel> documentCheckListItems = default;


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

    public List<ApplicationDocumentTypeViewModel> DocumentsTree { get => documentsTree; }
    public List<DocumentCheckListSectionViewModel> DocumentCheckListItems { get => documentCheckListItems; }

    #endregion
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
                        return mapper.Map<FloodApplicationDocumentEntity, ApplicationDocumentViewModel>(o);
                    }).ToList() ?? new List<ApplicationDocumentViewModel>()
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
