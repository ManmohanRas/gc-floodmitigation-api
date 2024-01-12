namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

class UpdateMunicipalCommentSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Flood].[FloodMunicipalComment]
                SET Comment = @p_Comment
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND AgencyId = @p_AgencyId;";

    public UpdateMunicipalCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
