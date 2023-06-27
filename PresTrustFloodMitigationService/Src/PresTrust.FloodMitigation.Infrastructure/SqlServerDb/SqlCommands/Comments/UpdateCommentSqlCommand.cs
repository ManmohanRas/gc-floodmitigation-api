namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

class UpdateCommentSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Flood].[FloodComment]
                SET Comment = @p_Comment
                    ,CommentTypeId = @p_CommentTypeId
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
