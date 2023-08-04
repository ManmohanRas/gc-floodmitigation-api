namespace PresTrust.FloodMitigation.Application.Commands;
public class SubmitDeclarationCommandHandler : BaseHandler, IRequestHandler<SubmitDeclarationCommand, SubmitDeclarationCommandViewModel>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;

    public SubmitDeclarationCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication
    ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SubmitDeclarationCommandViewModel> Handle(SubmitDeclarationCommand request, CancellationToken cancellationToken)
    {
        SubmitDeclarationCommandViewModel result = new ();

        // check if application exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        //update application
        if (application != null)
        {
            application.StatusId = (int)ApplicationStatusEnum.DOI_SUBMITTED;
            application.LastUpdatedBy = userContext.Email;
        }

        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplication.SaveApplicationWorkflowStatusAsync(application);
            FloodApplicationStatusLogEntity appStatusLog = new()
            {
                ApplicationId = application.Id,
                StatusId = application.StatusId,
                StatusDate = DateTime.Now,
                Notes = string.Empty,
                LastUpdatedBy = application.LastUpdatedBy
            };
            await repoApplication.SaveStatusLogAsync(appStatusLog);
            //change properties statuses to DOI_SUBMITTED in future

            scope.Complete();
            result.IsSuccess = true;
        }

        return result;
    }
}
