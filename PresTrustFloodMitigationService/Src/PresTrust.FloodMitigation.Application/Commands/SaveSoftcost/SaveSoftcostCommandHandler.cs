namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftCostCommandHandler : IRequestHandler<SaveSoftCostCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ISoftCostRepository repoSoftCost;
    private readonly IParcelFinanceRepository repoParcelFinance;

    public SaveSoftCostCommandHandler
        (
            IMapper mapper,
            IPresTrustUserContext userContext,
            IOptions<SystemParameterConfiguration> systemParamOptions,
            IApplicationRepository repoApplication,
            ISoftCostRepository repoSoftCost,
            IParcelFinanceRepository repoParcelFinance
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSoftCost = repoSoftCost;
        this.repoParcelFinance = repoParcelFinance;
    }

    public async Task<Unit> Handle(SaveSoftCostCommand request, CancellationToken cancellationToken)
    {
        decimal softCostFMPAmt = 0;
        foreach (var softCost in request.SoftCostLineItems)
        {
            var entity = mapper.Map<SaveSoftCostModel, FloodParcelSoftCostEntity>(softCost);
            entity.ApplicationId = request.ApplicationId;
            entity.PamsPin = request.PamsPin;
            await this.repoSoftCost.SaveAsync(entity);
            softCostFMPAmt += softCost.PaymentAmount;
        }

        var parcelFinance = await repoParcelFinance.GetParceFinanceAsync(request.ApplicationId, request.PamsPin);
        if(parcelFinance != null)
        {
            parcelFinance.ApplicationId = request.ApplicationId;
            parcelFinance.PamsPin = request.PamsPin;
            parcelFinance.SoftCostFMPAmt = softCostFMPAmt;
            await repoParcelFinance.SaveAsync(parcelFinance);
        }

        return Unit.Value;
    }
}
