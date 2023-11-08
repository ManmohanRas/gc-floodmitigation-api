using Newtonsoft.Json.Linq;

namespace PresTrust.FloodMitigation.Application.Commands;
public class SubmitPropertyCommandHandler : BaseHandler, IRequestHandler<SubmitPropertyCommand, SubmitPropertyCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoProperty;

    public SubmitPropertyCommandHandler
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
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SubmitPropertyCommandViewModel> Handle(SubmitPropertyCommand request, CancellationToken cancellationToken)
    {
        SubmitPropertyCommandViewModel result = new ();

        // check if application exists
        var Property = await GetIfPropertyExists(request.ApplicationId, request.Pamspin);

        //update application
        if (Property != null)
        {
            Property.StatusId = (int)PropertyStatusEnum.SUBMITTED;
            Property.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoProperty.SaveApplicationParcelWorkflowStatusAsync(Property);
            FloodParcelStatusLogEntity appStatusLog = new()
            {
                ApplicationId = Property.ApplicationId,
                PamsPin = Property.PamsPin,
                StatusId = Property.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = Property.LastUpdatedBy
            };
            await repoProperty.SaveStatusLogAsync(appStatusLog);
            //change properties statuses to submitted in future

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
