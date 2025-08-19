using OneOf.Types;
using static System.Net.Mime.MediaTypeNames;

namespace PresTrust.FloodMitigation.Application.Queries;

public class GetApplicationOverviewQueryHandler :BaseHandler, IRequestHandler<GetApplicationOverviewQuery, GetApplicationOverviewQueryViewModel>
{
    private readonly IMapper mapper;
    private readonly IApplicationOverviewRepository repoOverviewDetails;
    private readonly IApplicationFundingAgencyRepository repoFundingAgency;
    private readonly IApplicationDocumentRepository repoDocument;
    private readonly IApplicationRepository repoApplication;
    private readonly IPresTrustUserContext userContext;
    public GetApplicationOverviewQueryHandler(
             IMapper mapper,
             IPresTrustUserContext userContext,
             IApplicationOverviewRepository repoOverviewDetails,
             IApplicationFundingAgencyRepository repoFundingAgency,
             IApplicationRepository repoApplication,
            IApplicationDocumentRepository repoDocument

        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoOverviewDetails = repoOverviewDetails;
        this.repoFundingAgency = repoFundingAgency;
        this.repoApplication = repoApplication;
        this.repoDocument = repoDocument;

    }

    public async Task<GetApplicationOverviewQueryViewModel> Handle(GetApplicationOverviewQuery request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);

        FloodApplicationOverviewEntity results = default;

        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        var documents = await GetDocuments(application.Id);

        results = await this.repoOverviewDetails.GetOverviewDetailsAsync(request.ApplicationId);

        var overviewDetails = mapper.Map<FloodApplicationOverviewEntity, GetApplicationOverviewQueryViewModel>(results);

        overviewDetails.DocumentsTree = documents ?? new List<ApplicationDocumentTypeViewModel>();

        var fundingAgencies = await repoFundingAgency.GetFundingAgencies(request.ApplicationId);
       
        overviewDetails.FundingAgencies = mapper.Map<IEnumerable<FloodApplicationFundingAgencyEntity>, IEnumerable<FloodApplicationFundingAgencyViewModel>>(fundingAgencies);
        return overviewDetails;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    private async Task<List<ApplicationDocumentTypeViewModel>> GetDocuments(int applicationId)
    {
        var documents = await repoDocument.GetApplicationDocumentsAsync(applicationId, (int)ApplicationSectionEnum.OVERVIEW);

        List<ApplicationDocumentTypeViewModel> documentsTree = new List<ApplicationDocumentTypeViewModel>();
        if (documents != null)
        {
            var docBuilder = new ApplicationDocumentTreeBuilder(documents);
            documentsTree = docBuilder.DocumentsTree;
        }

        return documentsTree;
    }
}
