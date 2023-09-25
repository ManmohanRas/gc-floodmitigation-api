namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationSignatoryRepository
{
    Task<FloodApplicationSignatoryEntity> GetSignatoryAsync(int applicationId);
 
    Task<FloodApplicationSignatoryEntity> SaveAsync(FloodApplicationSignatoryEntity floodApplicationSignatory);
}
