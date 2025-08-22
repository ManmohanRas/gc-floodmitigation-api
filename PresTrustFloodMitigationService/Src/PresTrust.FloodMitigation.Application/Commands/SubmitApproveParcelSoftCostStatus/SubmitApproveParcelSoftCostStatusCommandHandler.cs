namespace PresTrust.FloodMitigation.Application.Commands;

public class SubmitApproveParcelSoftCostStatusCommandHandler : BaseHandler, IRequestHandler<SubmitApproveParcelSoftCostStatusCommand, bool>
{
    private readonly IMapper mapper;
    private readonly IApplicationRepository repoApplication;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationParcelRepository repoApplicationParcel;
    private readonly IEmailManager repoEmailManager;
    private readonly ISoftCostRepository repoSoftCost;
    private readonly IFinanceRepository repoApplicationFinance;
    private readonly IParcelFinanceRepository repoParcelFinance;
    private readonly IApplicationParcelRepository repoProperty;
    private readonly IPresTrustUserContext userContext;


    public SubmitApproveParcelSoftCostStatusCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IApplicationParcelRepository repoApplicationParcel,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ISoftCostRepository repoSoftCost,
        IEmailManager repoEmailManager,
        IFinanceRepository repoApplicationFinance,
        IParcelFinanceRepository repoParcelFinance,
        IApplicationParcelRepository repoProperty
        ) : base(repoApplication, repoProperty)
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.repoApplicationParcel = repoApplicationParcel;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoEmailManager = repoEmailManager;
        this.repoSoftCost = repoSoftCost;
        this.repoApplicationFinance = repoApplicationFinance;
        this.repoParcelFinance = repoParcelFinance;
        this.repoProperty = repoProperty;
    }
    public async Task<bool> Handle(SubmitApproveParcelSoftCostStatusCommand request, CancellationToken cancellationToken)
    {
        userContext.DeriveUserProfileFromUserId(request.UserId);
        // check if Property exists
        var application = await GetIfApplicationExists(request.ApplicationId);
        var property = await GetIfPropertyExists(request.ApplicationId, request.PamsPin);


        var reqParcelStatus = mapper.Map<SubmitApproveParcelSoftCostStatusCommand, FloodApplicationParcelEntity>(request);
        string emailTemplateCode = String.Empty;
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await repoApplicationParcel.UpdateApplicationParcelSoftCostStatus(reqParcelStatus);

            if (reqParcelStatus.IsSubmitted == true && reqParcelStatus.IsApproved == true)
            {
                var parcelFinance = await this.repoParcelFinance.GetParceFinanceAsync(request.ApplicationId, request.PamsPin);
                if (parcelFinance != null)
                {
                    var applicationFinance = await repoApplicationFinance.GetFinanceAsync(request.ApplicationId);
                    var softCostLineItems = await repoSoftCost.GetAllSoftCostLineItemsAsync(application.Id, request.PamsPin);
                    parcelFinance.SoftCostFMPAmt = softCostLineItems.Sum(o => o.PaymentAmount);
                    parcelFinance.SoftCostFMPAmt = (parcelFinance.SoftCostFMPAmt * applicationFinance.MatchPercent) / 100;
                    parcelFinance = await repoParcelFinance.SaveAsync(parcelFinance);
                }
            }

            if (reqParcelStatus.IsSubmitted == true && reqParcelStatus.IsApproved == false)
            {
                emailTemplateCode = EmailTemplateCodeTypeEnum.SUBMIT_SOFTCOST.ToString();
            }
            else 
            {
                emailTemplateCode = EmailTemplateCodeTypeEnum.APPROVE_SOFTCOST.ToString();
            }
            //Get Template and Send Email
           // await repoEmailManager.GetEmailTemplate(emailTemplateCode, application, property);

            scope.Complete();
        }


        return true;
    }
}
