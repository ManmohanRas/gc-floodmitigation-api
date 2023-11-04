using Microsoft.AspNetCore.Builder;

namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveTechDetailsCommandHandler : IRequestHandler<SaveTechDetailsCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private ITechDetailsRepository repoTechDetails;

    public SaveTechDetailsCommandHandler
   (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       ITechDetailsRepository repoTechDetails
   )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoTechDetails = repoTechDetails;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SaveTechDetailsCommand request, CancellationToken cancellationToken)
    {
        // map command object to the FloodTechDetailsEntity
        var reqTechDetails = mapper.Map<SaveTechDetailsCommand, FloodTechDetailsEntity>(request);
        reqTechDetails.LastUpdatedBy = userContext.Email;

        var techDetails = await repoTechDetails.SaveTechAsync(reqTechDetails);
        return techDetails.Id;
    }

}
