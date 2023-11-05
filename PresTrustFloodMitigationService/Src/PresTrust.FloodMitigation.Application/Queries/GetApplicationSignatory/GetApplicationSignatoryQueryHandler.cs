namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetApplicationSignatoryQueryHandler : BaseHandler, IRequestHandler<GetApplicationSignatoryQuery, GetApplicationSignatoryQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private IApplicationSignatoryRepository repoSignatory;

    public GetApplicationSignatoryQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        IApplicationSignatoryRepository repoSignatory
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoSignatory = repoSignatory;
    }

    public async Task<GetApplicationSignatoryQueryViewModel> Handle(GetApplicationSignatoryQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get signatory details
        var signatory = await this.repoSignatory.GetSignatoryAsync(request.ApplicationId);
        var result = mapper.Map<FloodApplicationSignatoryEntity, GetApplicationSignatoryQueryViewModel>(signatory);

        return result;  
    }
}
