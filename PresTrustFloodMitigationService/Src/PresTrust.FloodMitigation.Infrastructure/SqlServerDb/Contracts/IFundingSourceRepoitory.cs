namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFundingSourceRepoitory
{
    /// <summary>
    ///  Procedure to fetch all Funding source items by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Funding source items.</returns>
    Task<List<FloodFundingSourceEntity>> GetFundingSourcesAsync(int applicationId);
    /// <summary>
    /// Save Funding source item.
    /// </summary>
    /// <param name="fundingSource"></param>
    /// <returns></returns>
    Task<FloodFundingSourceEntity> SaveAsync(FloodFundingSourceEntity fundingSource);
    /// <summary>
    /// Delete Fudning Source.
    /// </summary>
    /// <returns></returns>
    Task DeleteAsync(FloodFundingSourceEntity fund);
}
