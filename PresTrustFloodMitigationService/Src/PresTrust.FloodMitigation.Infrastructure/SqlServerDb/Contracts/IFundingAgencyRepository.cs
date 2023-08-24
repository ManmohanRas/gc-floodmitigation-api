namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IFundingAgencyRepository
{
    /// <summary>
    /// Procedure to fetch application's fundingAgency for a given application status or all
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<IEnumerable<FloodFundingAgencyEntity>> GetFundingAgencies(int applicationId);
    // <summary>
    /// Save fundingAgency.
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns>Returns fundingAgency.</returns>
    Task<FloodFundingAgencyEntity> SaveAsync(FloodFundingAgencyEntity fundingAgency);
    /// <summary>
    /// Delete fundingAgency
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    Task DeleteAsync(FloodFundingAgencyEntity fundingAgency);
}
