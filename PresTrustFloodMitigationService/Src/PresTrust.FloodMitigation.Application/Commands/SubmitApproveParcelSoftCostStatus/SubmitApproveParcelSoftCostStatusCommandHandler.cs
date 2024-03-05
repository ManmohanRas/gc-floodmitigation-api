namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApproveParcelSoftCostStatusCommandHandler : BaseHandler, IRequestHandler<SubmitApproveParcelSoftCostStatusCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IEmailManager repoEmailManager;


    public SubmitApproveParcelSoftCostStatusCommandHandler(
        IMapper mapper,
        IApplicationParcelRepository repoApplicationParcel,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        IEmailManager repoEmailManager
        ) : base(repoApplication)
    {
        this.mapper = mapper;
        this.repoApplicationParcel = repoApplicationParcel;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoEmailManager = repoEmailManager;
    }
    public async Task<bool> Handle(SubmitApproveParcelSoftCostStatusCommand request, CancellationToken cancellationToken)
    {
        // check if Property exists
        var application = await GetIfApplicationExists(request.ApplicationId);

        var reqParcelStatus = mapper.Map<SubmitApproveParcelSoftCostStatusCommand, FloodApplicationParcelEntity>(request);
        string emailTemplateCode = String.Empty;
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplicationParcel.UpdateApplicationParcelSoftCostStatus(reqParcelStatus);

            if (reqParcelStatus.IsSubmitted == true)
            {
                emailTemplateCode = EmailTemplateCodeTypeEnum.SUBMIT_SOFTCOST.ToString();
            }
            else if (reqParcelStatus.IsSubmitted == true && reqParcelStatus.IsApproved == true)
            {
                emailTemplateCode = EmailTemplateCodeTypeEnum.APPROVE_SOFTCOST.ToString();
            }
            //Get Template and Send Email
            await repoEmailManager.GetEmailTemplate(emailTemplateCode, application);

            scope.Complete();
        }
            

        return true;
    }
}
