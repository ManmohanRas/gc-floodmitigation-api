namespace PresTrust.FloodMitigation.Application.Queries;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class GetSignatoryQueryHandler : IRequestHandler<GetSignatoryQuery, GetSignatoryQueryViewModel>
{
    private IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private ISignatureRepository repoSignature;
    
    public GetSignatoryQueryHandler(
        IMapper mapper,
        IApplicationRepository repoApplication,
        ISignatureRepository repoSignature
        ) 
    {
        this.mapper = mapper;
        this.repoApplication = repoApplication;
        this.repoSignature = repoSignature;
    }

    public async Task<GetSignatoryQueryViewModel> Handle(GetSignatoryQuery request, CancellationToken cancellationToken)
    {
        // get signature details
        var signature = await this.repoSignature.GetSignatureAsync(request.ApplicationId);
        signature = signature ?? new FloodSignatoryEntity();
       
        var result = mapper.Map<FloodSignatoryEntity, GetSignatoryQueryViewModel>(signature);

        return result;  
    }
}
