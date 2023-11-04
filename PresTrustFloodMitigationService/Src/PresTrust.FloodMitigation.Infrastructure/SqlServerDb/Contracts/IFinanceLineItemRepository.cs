namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFinanceLineItemRepository
{
    /// <summary>
    ///  Procedure to fetch all Finance line items by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Finance line items.</returns>
    Task<List<FloodFinanceLineItemEntity>> GetFinanceLineItemsAsync(int applicationId);

    /// <summary>
    /// Save Finance line item.
    /// </summary>
    /// <param name="financeLineItem"></param>
    /// <returns></returns>
    Task<FloodFinanceLineItemEntity> SaveAsync(FloodFinanceLineItemEntity financeLineItem);
}
