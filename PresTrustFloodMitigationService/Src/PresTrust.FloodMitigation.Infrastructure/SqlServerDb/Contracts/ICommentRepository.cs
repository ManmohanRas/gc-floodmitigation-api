namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ICommentRepository
{
    /// <summary>
    ///  Procedure to fetch all Comments by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Comments.</returns>
    Task<IEnumerable<FloodCommentsEntity>> GetAllCommentsAsync(int applicationId);

   
    /// <summary>
    /// Save Comment.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task<FloodCommentsEntity> SaveAsync(FloodCommentsEntity comment);
    
    /// <summary>
    /// Delete Comment.
    /// </summary>
    /// <returns></returns>
    Task DeleteCommentAsync(FloodCommentsEntity comment);
}
