namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IContactRepository
{
    /// <summary>
    ///  Procedure to fetch all contacts by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns contacts.</returns>
    Task<List<FloodContactEntity>> GetAllContactsAsync(int applicationId);

    /// <summary>
    /// Save Contact.
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    Task<FloodContactEntity> SaveAsync(FloodContactEntity contact);

    /// <summary>
    /// Delete Contact.
    /// </summary>
    /// <returns></returns>
    Task DeleteContactAsync(FloodContactEntity contact);


}
