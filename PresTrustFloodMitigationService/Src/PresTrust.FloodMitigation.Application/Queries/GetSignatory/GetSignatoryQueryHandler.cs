namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetSignatoryQueryHandler : BaseHandler, IRequestHandler<GetSignatoryQuery, GetSignatoryQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private ISignatoryRepository repoSignatory;

    public GetSignatoryQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        ISignatoryRepository repoSignatory
        ) : base(repoApplication: repoApplication)
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoSignatory = repoSignatory;
    }

    public async Task<GetSignatoryQueryViewModel> Handle(GetSignatoryQuery request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        // get signature details
        var signatory = await this.repoSignatory.GetSignatoryAsync(request.ApplicationId);
        signatory = signatory ?? new FloodSignatoryEntity()
        {
            ApplicationId = application.Id
        };
        var result = mapper.Map<FloodSignatoryEntity, GetSignatoryQueryViewModel>(signatory);

        return result;  
    }
}
