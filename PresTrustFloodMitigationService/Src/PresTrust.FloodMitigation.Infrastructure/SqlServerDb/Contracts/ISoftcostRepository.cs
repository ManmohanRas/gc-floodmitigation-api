namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ISoftCostRepository
{
    Task<List<FloodParcelSoftCostEntity>> GetAllSoftCostLineItemsAsync(int applicationId, string pamsPin);
    Task<FloodParcelSoftCostEntity> SaveAsync(FloodParcelSoftCostEntity softcost);
}
