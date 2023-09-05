namespace PresTrust.FloodMitigation.Application.Commands;

public class SaveSoftcostCommandHandler : IRequestHandler<SaveSoftcostCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly IPresTrustUserContext userContext;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IApplicationRepository repoApplication;
    private readonly ISoftcostRepository repoSoftcost;

    public SaveSoftcostCommandHandler
        (
         IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IApplicationRepository repoApplication,
        ISoftcostRepository repoSoftcost
        )
    {
        this.mapper = mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoApplication = repoApplication;
        this.repoSoftcost = repoSoftcost;
    }

    public async Task<Unit> Handle(SaveSoftcostCommand request, CancellationToken cancellationToken)
    {
        var softCostLineItems = request.SoftcostLineItems ?? new List<FloodParcelSoftcostViewModel>();

        foreach (var softCost in softCostLineItems)
        {
            var entity = mapper.Map<FloodParcelSoftcostViewModel, FloodParcelSoftcostEntity>(softCost);

            await this.repoSoftcost.SaveAsync(entity);

        }

        return Unit.Value;
    }
}
