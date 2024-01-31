namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
public class CreateParcelAuditDialogSqlCommand
{
    private readonly string _sqlCommand =
                    @"INSERT INTO [Flood].[FloodParcelAuditDialog]
              (
                 Section
                ,Partial
                ,Acres
                ,AcresToBeAcquired
                ,IsThisAnExclusionArea
                ,Notes
                ,InterestType
                ,EasementId
                ,ChangeType
                ,ChangeDate
                ,ReasonForChange
                ,IsActive
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_Section
               ,@p_Partial
               ,@p_Acres
               ,@p_AcresToBeAcquired
               ,@p_IsThisAnExclusionArea
               ,@p_Notes
               ,@p_InterestType
               ,@p_EasementId
               ,@p_ChangeType
               ,@p_ChangeDate
               ,@p_ReasonForChange
               ,1
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateParcelAuditDialogSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}