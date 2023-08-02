namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveSignatoryCommandHandler : IRequestHandler<SaveSignatoryCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackRepository repoFeedback;
    private ISignatoryRepository repoSignatory;

    public SaveSignatoryCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ISignatoryRepository repoSignatory,
        IFeedbackRepository repoFeedback
    ) 
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSignatory = repoSignatory;
        this.repoFeedback = repoFeedback;
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(SaveSignatoryCommand request, CancellationToken cancellationToken)
    {
        int signatoryId = 0;

        // map command object to the FloodSignatoryEntity
        var reqSignatory = mapper.Map<SaveSignatoryCommand, FloodSignatoryEntity>(request);

        var signatory = await repoSignatory.SaveAsync(reqSignatory);
        if (signatory != null)
        {
            signatoryId = signatory.Id;
        }
       

        return signatoryId;
    }
}
