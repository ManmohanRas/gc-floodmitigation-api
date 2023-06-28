namespace PresTrust.FloodMitigation.Application.Commands;
public class SaveSignatoryCommandHandler : IRequestHandler<SaveSignatoryCommand, int>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly IFeedbackRepository repoFeedback;
    private ISignatureRepository repoSignature;

    public SaveSignatoryCommandHandler
    (
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ISignatureRepository repoSignature,
        IFeedbackRepository repoFeedback
    ) 
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSignature = repoSignature;
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
        int signatureId = 0;

        // map command object to the FloodSignatoryEntity
        var reqSignature = mapper.Map<SaveSignatoryCommand, FloodSignatoryEntity>(request);

        var signature = await repoSignature.SaveAsync(reqSignature);
        if (signature != null)
        {
            signatureId = signature.Id;
        }
       

        return signatureId;
    }
}
