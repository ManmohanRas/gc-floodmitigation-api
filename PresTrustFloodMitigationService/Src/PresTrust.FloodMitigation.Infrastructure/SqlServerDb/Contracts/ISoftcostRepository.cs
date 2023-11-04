namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ISoftcostRepository
{
    Task<List<FloodParcelSoftcostEntity>> GetAllSoftcostLineItemsAsync(int applicationId, string pamsPin);
    Task<FloodParcelSoftcostEntity> SaveAsync(FloodParcelSoftcostEntity softcost);
}
