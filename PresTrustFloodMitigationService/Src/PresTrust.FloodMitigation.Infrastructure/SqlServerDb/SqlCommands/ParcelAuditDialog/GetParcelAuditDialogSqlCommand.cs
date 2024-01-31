namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;
public class GetParcelAuditDialogSqlCommand
{
    private readonly string _sqlCommand =
       @"  SELECT [Id]
                      ,[AgencyId]
                      ,[Block]
                      ,[Lot]
                      ,[QualificationCode]
                      ,[PamsPin]
                      ,[Section]
                      ,[Acres]
                      ,[ChangeDate]
                      ,[PamsPinStatus]
                      ,[LastUpdatedOn]
                      ,[LastUpdatedBy]
                FROM [Flood].[FloodParcelAuditDialog] 
                WHERE [AgencyId] = @p_AgencyId;"
       ;
    public GetParcelAuditDialogSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
    
    

