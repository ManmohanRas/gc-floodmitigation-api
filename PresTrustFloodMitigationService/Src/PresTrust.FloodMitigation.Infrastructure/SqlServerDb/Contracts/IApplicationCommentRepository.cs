namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface IApplicationCommentRepository
{
    /// <summary>
    ///  Procedure to fetch all Comments by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Comments.</returns>
    Task<IEnumerable<FloodApplicationCommentEntity>> GetAllCommentsAsync(int applicationId);

   
    /// <summary>
    /// Save Comment.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task<FloodApplicationCommentEntity> SaveAsync(FloodApplicationCommentEntity comment);
    
    /// <summary>
    /// Delete Comment.
    /// </summary>
    /// <returns></returns>
    Task DeleteCommentAsync(FloodApplicationCommentEntity comment);
}
