namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;
public interface IParcelAuditDialog
{
    /// <summary>
    ///  Procedure to fetch all parcel audit  by agencyId.
    /// </summary>
    /// <param name="agencyId"> Agency Id.</param>
    
    Task<List<FloodParcelAuditDialogEntity>> GetParcelAuditDialogAsync(int agencyId);


    /// <summary>
    /// Save Comment.
    /// </summary>
    /// <param name="dialog"></param>
    /// <returns></returns>
    Task<FloodParcelAuditDialogEntity> SaveParcelAuditDialogAsync(FloodParcelAuditDialogEntity dialog);
}
    

