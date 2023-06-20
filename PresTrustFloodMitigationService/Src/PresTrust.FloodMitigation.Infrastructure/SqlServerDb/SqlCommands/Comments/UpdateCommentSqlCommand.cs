namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Comments;

class UpdateCommentSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Hist].[HistComment]
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
