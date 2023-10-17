namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ISoftcostRepository
{
    Task<IEnumerable<FloodParcelSoftcostEntity>> GetAllSoftcostLineItemsAsync(int applicationId, string pamsPin);
    Task<FloodParcelSoftcostEntity> SaveAsync(FloodParcelSoftcostEntity softcost);

    Task<IEnumerable<Dictionary<string, decimal>>> GetSoftcostSumsByTypeId(int applicationId, string pamsPin);
}
