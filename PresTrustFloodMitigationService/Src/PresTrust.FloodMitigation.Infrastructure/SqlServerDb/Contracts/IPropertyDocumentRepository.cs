namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IPropertyDocumentRepository
{
    
    /// <summary>
    /// Procedure to delete document 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeletePropertyDocumentAsync(int id);

    /// <summary>
    /// Procedure to fetch Document by PamsPin
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sectionId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<List<FloodPropertyDocumentEntity>> GetPropertyDocumentsAsync(int applicationId, string pamsPin, int sectionId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    Task<FloodPropertyDocumentEntity> SavePropertyDocumentDetailsAsync(FloodPropertyDocumentEntity parcelDoc);
    
    /// <summary>
    /// Procedure to update application document checklist
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    Task<FloodPropertyDocumentEntity> SavePropertyDocumentChecklistAsync(FloodPropertyDocumentEntity doc);

    /// <summary>
    /// Procedure to get Property document checklist
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="pamsPin"></param>
    /// <returns></returns>
    Task<List<FloodPropertyDocumentEntity>> GetPropertyDocumentChecklistAsync(int applicationId, string pamsPin);
}

