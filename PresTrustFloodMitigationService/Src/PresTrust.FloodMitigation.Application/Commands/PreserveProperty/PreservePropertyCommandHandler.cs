namespace PresTrust.FloodMitigation.Application.Commands;
public class PreservePropertyCommandHandler : BaseHandler, IRequestHandler<PreservePropertyCommand, PreservePropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;

    public PreservePropertyCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IApplicationParcelRepository repoProperty
    ) : base(repoApplication, repoProperty)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoProperty = repoProperty;        
    }

    /// <summary>
    /// 
    /// </summary>....................... 
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PreservePropertyCommandViewModel> Handle(PreservePropertyCommand request, CancellationToken cancellationToken)
    {
        PreservePropertyCommandViewModel result = new ();

        // check if Property exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.Pamspin);

        //update Property
        if (Property != null)
        {
            Property.StatusId = (int)PropertyStatusEnum.PRESERVED;
            Property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(Property);
            FloodApplicationParcelStatusLogEntity appStatusLog = new()
            {
                ApplicationId = Property.ApplicationId,
                PamsPin = Property.PamsPin,
                StatusId = Property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = Property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appStatusLog);
            //change properties statuses to in-Preserve in future

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
