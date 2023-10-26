using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SavePropertyDetailsCommandHandler : BaseHandler, IRequestHandler<SavePropertyDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private IPropertyAdminDetailsRepository repoPropertyDetails;

    public SavePropertyDetailsCommandHandler 
    (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       IPropertyAdminDetailsRepository repoPropertyDetails
    )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoPropertyDetails = repoPropertyDetails;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SavePropertyDetailsCommand request, CancellationToken cancellationToken)
    {
        // map command object to the FloodPropertyDetailsEntity
        
        var reqPropDetails = mapper.Map<SavePropertyDetailsCommand, FloodPropertyAdminDetailsEntity>(request);
        reqPropDetails.LastUpdatedBy = userContext.Email;

        reqPropDetails = await repoPropertyDetails.SaveAsync(reqPropDetails);

        return reqPropDetails.Id;
    }
}
