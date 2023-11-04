using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresTrust.FloodMitigation.Application.Commands;


public class SaveParcelPropertyCommandHandler : IRequestHandler<SaveParcelPropertyCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private IParcelPropertyRepository repoParcelProperty;

    public SaveParcelPropertyCommandHandler
   (
       IMapper mapper,
       IPresTrustUserContext userContext,
       IOptions<SystemParameterConfiguration> systemParamOptions,
       IApplicationRepository repoApplication,
       IParcelPropertyRepository repoParcelProperty
   )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoParcelProperty = repoParcelProperty;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SaveParcelPropertyCommand request, CancellationToken cancellationToken)
    {
        // map command object to the FloodParcelPropertyEntity
        var reqParcelProperty = mapper.Map<SaveParcelPropertyCommand, FloodParcelPropertyEntity>(request);
        reqParcelProperty.LastUpdatedBy = userContext.Email;

        var ParcelProperty = await repoParcelProperty.SavePropertyAsync(reqParcelProperty);
        return ParcelProperty.Id;
    }

}

