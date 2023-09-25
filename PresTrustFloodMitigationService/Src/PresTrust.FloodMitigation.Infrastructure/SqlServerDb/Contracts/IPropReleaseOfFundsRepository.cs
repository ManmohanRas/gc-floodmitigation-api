namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public interface IPropReleaseOfFundsRepository
{
    /// <summary>
    ///  Procedure to fetch grant details by Id.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Grant.</returns>
    Task<FloodPropReleaseOfFundsEntity> GetReleaseOfFundsAsync(int applicationId, int Pamspin);
}
