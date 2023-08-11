namespace PresTrust.FloodMitigation.Application.Commands;

/// <summary>
/// This class handles the query to fetch data and build response
/// </summary>
public class SaveApplicationFinanceCommandHandler : BaseHandler, IRequestHandler<SaveApplicationFinanceCommand, int>
{
    private IMapper mapper;
    private readonly SystemParameterConfiguration systemParamOptions;
    private readonly IPresTrustUserContext userContext;
    private readonly IFinanceRepository repoFinance;
    private readonly IFundingSourceRepoitory repoFundingSource;
    private readonly IFinanceLineItemRepository repoFinanceLineItem;
    private readonly IApplicationRepository repoApplication;


    public SaveApplicationFinanceCommandHandler(
        IMapper mapper,
        IPresTrustUserContext userContext,
        IOptions<SystemParameterConfiguration> systemParamOptions,
        IFinanceRepository repoFinance,
        IFundingSourceRepoitory repoFundingSource,
        IFinanceLineItemRepository repoFinanceLineItem,
        IApplicationRepository repoApplication
        ) : base(repoApplication: repoApplication)
    {
        this.mapper =   mapper;
        this.userContext = userContext;
        this.systemParamOptions = systemParamOptions.Value;
        this.repoFinance = repoFinance;
        this.repoFundingSource = repoFundingSource;
        this.repoFinanceLineItem = repoFinanceLineItem;
        this.repoApplication = repoApplication;
    }
    public async Task<int> Handle(SaveApplicationFinanceCommand request, CancellationToken cancellationToken)
    {
        // get application details
        var application = await GetIfApplicationExists(request.ApplicationId);

        int financeId = 0;

        var reqFinance = mapper.Map<SaveApplicationFinanceCommand, FloodApplicationFinanceEntity>(request);
        reqFinance.LastUpdatedBy = userContext.Email;

        var reqFundingSource = new List<FloodFundingSourceViewModel>(request.FundingSources ?? new List<FloodFundingSourceViewModel>());
        var reqFinanceLineItems = new List<FloodFinanceLineItemViewModel>(request.FinanceLineItems ?? new List<FloodFinanceLineItemViewModel>());

        // save grant, grant worksheet items, grant matching funds and broken rules if any
        using (var scope = TransactionScopeBuilder.CreateReadCommitted(systemParamOptions.TransScopeTimeOutInMinutes))
        {

            reqFinance = await repoFinance.SaveAsync(reqFinance);
            await SaveFundingSources(reqFundingSource);
            await SaveFinanceLineItems(reqFinanceLineItems);

            if (reqFinance != null)
            {
                financeId = reqFinance.Id;
            }

            scope.Complete();

        }

        return financeId;
    }

    private async Task SaveFundingSources(IEnumerable<FloodFundingSourceViewModel> fundingSources)
    {
        foreach (var fundingSource in fundingSources)
        {
            var entity = mapper.Map<FloodFundingSourceViewModel, FloodFundingSourceEntity>(fundingSource);
            
                await repoFundingSource.SaveAsync(entity);
            
        }
    }

    private async Task SaveFinanceLineItems(IEnumerable<FloodFinanceLineItemViewModel> financeLinteItems)
    {
        foreach (var lineItem in financeLinteItems)
        {
            var entity = mapper.Map<FloodFinanceLineItemViewModel, FloodFinanceLineItemEntity>(lineItem);

            await repoFinanceLineItem.SaveAsync(entity);

        }
    }
}
