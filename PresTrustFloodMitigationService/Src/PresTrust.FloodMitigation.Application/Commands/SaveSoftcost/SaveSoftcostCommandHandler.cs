namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftcostCommandHandler : IRequestHandler<SaveSoftcostCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ISoftcostRepository repoSoftcost;
    private readonly IParcelFinanceRepository repoParcelFinance;

    public SaveSoftcostCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            ISoftcostRepository repoSoftcost,
            IParcelFinanceRepository repoParcelFinance
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSoftcost = repoSoftcost;
        this.repoParcelFinance = repoParcelFinance;
    }

    public async Task<Unit> Handle(SaveSoftcostCommand request, CancellationToken cancellationToken)
    {
        var softCostLineItems = request.SoftcostLineItems ?? new List<FloodParcelSoftcostViewModel>();

        int applicationId = 0; // need to revisit
        string pamsPin = string.Empty; // need to revisit
        decimal softCostFMPAmt = 0;
        foreach (var softCost in softCostLineItems)
        {
            applicationId = softCost.ApplicationId;
            pamsPin = softCost.PamsPin;
            var entity = mapper.Map<FloodParcelSoftcostViewModel, FloodParcelSoftcostEntity>(softCost);
            await this.repoSoftcost.SaveAsync(entity);
            softCostFMPAmt += softCost.PaymentAmount;
        }

        var parcelFinance = await repoParcelFinance.GetParceFinanceAsync(applicationId, pamsPin);
        if(parcelFinance != null)
        {
            parcelFinance.SoftCostFMPAmt = softCostFMPAmt;
            await repoParcelFinance.SaveAsync(parcelFinance);
        }

        return Unit.Value;
    }
}
