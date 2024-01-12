namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;
public interface IMunicipalCommentRepository
{
    /// <summary>
    ///  Procedure to fetch all Comments by agencyId.
    /// </summary>
    /// <param name="agencyId"> Agency Id.</param>
    /// <returns> Returns Comments.</returns>
    Task<List<FloodMunicipalCommentEntity>> GetAllCommentsAsync(int agencyId);


    /// <summary>
    /// Save Comment.
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    Task<FloodMunicipalCommentEntity> SaveAsync(FloodMunicipalCommentEntity comment);

    /// <summary>
    /// Delete Comment.
    /// </summary>
    /// <returns></returns>
    Task DeleteCommentAsync(FloodMunicipalCommentEntity comment);

}

