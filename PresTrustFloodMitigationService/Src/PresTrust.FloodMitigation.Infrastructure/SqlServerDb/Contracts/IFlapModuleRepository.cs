namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

public interface IFlapModuleRepository
{
    /// <summary>
    /// Get flap details
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    Task<FloodFlapEntity> GetFlapAsync(int agencyId);

    /// <summary>
    /// Get flap comments
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    Task<List<FloodFlapCommentEntity>> GetFlapCommentsAsync(int agencyId);

    /// <summary>
    /// Save flap details
    /// </summary>
    /// <param name="flap"></param>
    /// <returns></returns>
    Task<FloodFlapEntity> SaveFlapAsync(FloodFlapEntity flap);

    /// <summary>
    /// Save flap comment
    /// </summary>
    /// <param name="flapComment"></param>
    /// <returns></returns>
    Task<FloodFlapCommentEntity> SaveFlapCommentAsync(FloodFlapCommentEntity flapComment);

    /// <summary>
    /// Get flap documents
    /// </summary>
    /// <param name="agencyId"></param>
    /// <returns></returns>
    Task<List<FloodFlapDocumentEntity>> GetFlapDocumentsAsync(int agencyId);

    /// <summary>
    /// Save flap document
    /// </summary>
    /// <param name="doc"></param>
    /// <returns></returns>
    Task<FloodFlapDocumentEntity> SaveFlapDocumentAsync(FloodFlapDocumentEntity doc);

    /// <summary>
    /// Delete flap document
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteFlapDocumentAsync(int id);

    /// <summary>
    /// Delete flap comment
    /// </summary>
    /// <param name="flapComment"></param>
    /// <returns></returns>
    Task DeleteFlapCommentAsync(FloodFlapCommentEntity flapComment);

}
