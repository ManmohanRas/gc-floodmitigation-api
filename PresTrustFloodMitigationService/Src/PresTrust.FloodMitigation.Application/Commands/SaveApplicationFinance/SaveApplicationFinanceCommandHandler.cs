namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class SaveApplicationFinanceCommandHandler : IRequestHandler<SaveApplicationFinanceCommand, int>
{
    private IMapper mapper;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPresTrustUserContext userContext;
    private readonly IFinanceRepository repoFinance;
    private readonly IFundingSourceRepoitory repoFundingSource;

    public SaveApplicationFinanceCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IFinanceRepository repoFinance,
        IFundingSourceRepoitory repoFundingSource
        )
    {
        this.mapper =   mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoFinance = repoFinance;
        this.repoFundingSource = repoFundingSource;
    }
    public async Task<int> Handle(SaveApplicationFinanceCommand request, CancellationToken cancellationToken)
    {
        int financeId = 0;

        var reqFinance = mapper.Map<SaveApplicationFinanceCommand, FloodApplicationFinanceEntity>(request);
        reqFinance.LastUpdatedBy = userContext.Email;
        reqFinance = await repoFinance.SaveAsync(reqFinance);

        // save grant, grant worksheet items, grant matching funds and broken rules if any
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {
            await SaveFundingSources(request.FundingSources);

            scope.Complete();

        }
        if (reqFinance != null)
        {
            financeId = reqFinance.Id;
        }

        return financeId;
    }

    private async Task SaveFundingSources(IEnumerable<FloodFundingSourceViewModel> fundingSources)
    {
        foreach (var fundingSource in fundingSources)
        {
            var entity = mapper.Map<FloodFundingSourceViewModel, FloodFundingSourceEntity>(fundingSource);

            if (fundingSource.RowStatus.EndsWith("D", StringComparison.OrdinalIgnoreCase))
            {
                await repoFundingSource.DeleteAsync(entity);
            }else
            {
                await repoFundingSource.SaveAsync(entity);
            }
        }
    }
}
