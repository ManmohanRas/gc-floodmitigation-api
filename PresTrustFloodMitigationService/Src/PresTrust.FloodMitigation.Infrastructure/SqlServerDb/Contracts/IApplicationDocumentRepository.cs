namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationDocumentRepository
{
    /// <summary>
    ///  Procedure to fetch Document by Application Id.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Document collection.</returns>
    Task<IEnumerable<FloodApplicationDocumentEntity>> GetApplicationDocumentsAsync(int applicationId, int sectionId);

    /// <summary>
    /// Procedure to save uploaded application document details
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    Task<FloodApplicationDocumentEntity> SaveApplicationDocumentDetailsAsync(FloodApplicationDocumentEntity doc);

    /// <summary>
    /// Procedure to delete application document
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteApplicationDocumentAsync(int id);

    /// <summary>
    /// Procedure to update application document checklist
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    Task<FloodApplicationDocumentEntity> UpdateDocumentCheckListItemsAsync(FloodApplicationDocumentEntity doc);

    /// <summary>
    /// Procedure to get Property document checklist
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<IEnumerable<FloodApplicationDocumentEntity>> GetDocumentCheckListAsync(int applicationId);
}
