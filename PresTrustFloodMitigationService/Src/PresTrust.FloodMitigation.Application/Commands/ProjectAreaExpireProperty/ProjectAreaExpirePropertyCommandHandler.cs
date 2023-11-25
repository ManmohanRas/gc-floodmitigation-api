namespace PresTrust.FloodMitigation.Application.Commands;
public class ProjectAreaExpirePropertyCommandHandler : BaseHandler, IRequestHandler<ProjectAreaExpirePropertyCommand, ProjectAreaExpirePropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;

    public ProjectAreaExpirePropertyCommandHandler
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
    public async Task<ProjectAreaExpirePropertyCommandViewModel> Handle(ProjectAreaExpirePropertyCommand request, CancellationToken cancellationToken)
    {
        ProjectAreaExpirePropertyCommandViewModel result = new ();

        // check if Property exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);

        //update Property
        if (Property != null)
        {
            Property.StatusId = (int)PropertyStatusEnum.PROJECT_AREA_EXPIRED;
            Property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(Property);
            FloodParcelStatusLogEntity appParcelStatusLog = new()
            {
                ApplicationId = Property.ApplicationId,
                PamsPin = Property.PamsPin,
                StatusId = Property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = Property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appParcelStatusLog);

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
