namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ICommentRepository
{
    /// <summary>
    ///  Procedure to fetch all Comments by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Comments.</returns>
    Task<IEnumerable<FloodCommentEntity>> GetAllCommentsAsync(int applicationId);

   
    /// <summary>
    /// Save Comment.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task<FloodCommentEntity> SaveAsync(FloodCommentEntity comment);
    
    /// <summary>
    /// Delete Comment.
    /// </summary>
    /// <returns></returns>
    Task DeleteCommentAsync(FloodCommentEntity comment);
}
