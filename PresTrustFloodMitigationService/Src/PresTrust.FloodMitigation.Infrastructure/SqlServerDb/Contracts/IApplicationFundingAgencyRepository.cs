namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationFundingAgencyRepository
{
    /// <summary>
    /// Procedure to fetch application's fundingAgency for a given application status or all
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<List<FloodApplicationFundingAgencyEntity>> GetFundingAgencies(int applicationId);
    // <summary>
    /// Save fundingAgency.
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns>Returns fundingAgency.</returns>
    Task<FloodApplicationFundingAgencyEntity> SaveAsync(FloodApplicationFundingAgencyEntity fundingAgency);
    /// <summary>
    /// Delete fundingAgency
    /// </summary>
    /// <param name="fundingAgency"></param>
    /// <returns></returns>
    Task DeleteAsync(FloodApplicationFundingAgencyEntity fundingAgency);
}
