namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationAdminDetailsQueryHandler : BaseHandler, IRequestHandler<GetApplicationAdminDetailsQuery, GetApplicationAdminDetailsQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IApplicationDetailsRepository repoDetails;
    private IPresTrustUserContext userContext;
    private readonly IApplicationDocumentRepository repoDocument;
    public GetApplicationAdminDetailsQueryHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationRepository repoApplication,
        IApplicationDetailsRepository repoDetails,
        IApplicationDocumentRepository repoDocument
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoApplication = repoApplication;
        this.repoDetails = repoDetails;
        this.repoDocument = repoDocument;
    }

    public async Task<GetApplicationAdminDetailsQueryViewModel> Handle(GetApplicationAdminDetailsQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        //get application details
        var application = await GetIfApplicationExists(request.ApplicationId);
        var documents =  await GetDocuments(request.ApplicationId);


        //get Admin details
        var details = await this.repoDetails.GetAsync(request.ApplicationId);
        var result = mapper.Map<FloodApplicationAdminDetailsEntity, GetApplicationAdminDetailsQueryViewModel>(details);
        result.DocumentsTree = documents ?? new List<ApplicationDocumentTypeViewModel>();

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    private async Task<List<ApplicationDocumentTypeViewModel>> GetDocuments(int applicationId)
    {
        var documents = await repoDocument.GetApplicationDocumentsAsync(applicationId, (int)ApplicationSectionEnum.ADMIN_DETAILS);

        List<ApplicationDocumentTypeViewModel> documentsTree = new List<ApplicationDocumentTypeViewModel>();
        if (documents != null)
        {
            var docBuilder = new ApplicationDocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }

        return documentsTree;
    }
}