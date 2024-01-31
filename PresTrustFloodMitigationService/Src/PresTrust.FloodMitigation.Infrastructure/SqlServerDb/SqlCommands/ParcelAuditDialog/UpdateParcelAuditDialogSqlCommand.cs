namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
 public class UpdateParcelAuditDialogSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Flood].[FloodParcelAuditDialog]
                SET AgencyId = @p_AgencyId
                   ,Block   =@p_Block
                   ,Lot     =@p_Lot
                   ,QualificationCode=@p_QualificationCode
                   ,PamsPin=@p_PamsPin
                   ,Section=@p_Section
                   ,Partial=@p_Partial
                   ,Acres  =@p_Acres
                   ,AcresToBeAcquired=@p_AcresToBeAcquired
                   ,IsThisAnExclusionArea=@p_IsThisAnExclusionArea
                   ,Notes=@p_Notes
                   ,InterestType=@p_InterestType
                   ,EasementId=@p_EasementId
                   ,ChangeType=@p_ChangeType
                   ,ChangeDate=@p_ChangeDate
                   ,PamsPinStatus=@p_PamsPinStatus
                   ,ReasonForChange=@p_ReasonForChange
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND AgencyId = @p_AgencyId;";

    public UpdateParcelAuditDialogSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
    

