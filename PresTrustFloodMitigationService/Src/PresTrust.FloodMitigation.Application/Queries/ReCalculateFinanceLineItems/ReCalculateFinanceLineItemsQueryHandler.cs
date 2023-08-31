namespace PresTrust.FloodMitigation.Application.Queries;

public class ReCalculateFinanceLineItemsQueryHandler: IRequestHandler<ReCalculateFinanceLineItemsQuery, ReCalculateFinanceLineItemsQueryViewModel>
{
    public ReCalculateFinanceLineItemsQueryHandler()
    {

    }
    public Task<ReCalculateFinanceLineItemsQueryViewModel> Handle(ReCalculateFinanceLineItemsQuery request, CancellationToken cancellationToken)
    {
        var result =  new ReCalculateFinanceLineItemsQueryViewModel();

        var financeLineItems = request.FinanceLineItems ?? new List<FloodFinanceLineItemViewModel>();

        financeLineItems.ToList().ForEach(item =>
        {
            item.FundsRequested = item.ValueEstimate * (request.MatchPercent / 100);
            item.MunicipalMatch = item.ValueEstimate - item.FundsRequested;
        });

        result = new ReCalculateFinanceLineItemsQueryViewModel()
        {
            Id = request.Id,
            ApplicationId = request.ApplicationId,
            MatchPercent  =  request.MatchPercent,
            FinanceLineItems = financeLineItems,
            FundingSources = request.FundingSources
        };

        return Task.FromResult(result);
        
    }
}
