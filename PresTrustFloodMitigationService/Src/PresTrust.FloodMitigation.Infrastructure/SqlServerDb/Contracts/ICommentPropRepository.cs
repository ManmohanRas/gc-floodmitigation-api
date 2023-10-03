namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;

public interface ICommentPropRepository
{
    /// <summary>
    ///  Procedure to fetch all Comments by applicationId.
    /// </summary>
    /// <param name="applicationId"> Application Id.</param>
    /// <returns> Returns Comments.</returns>
    Task<IEnumerable<FloodPropertyCommentEntity>> GetCommentsAsync(int applicationId, string Pamspin);
    /// <summary>
    /// Save Comment.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task<FloodPropertyCommentEntity> SaveCommentsAsync(FloodPropertyCommentEntity comment);
    /// <summary>
    /// Delete Comment.
    /// </summary>
    /// <returns></returns>
    Task DeleteCommentAsync(FloodPropertyCommentEntity comment);
}
