namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Comments;

class UpdateConsultantCommentSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Hist].[HistConsultantComment]
                SET Comment = @p_Comment
                    ,CommentTypeId = @p_CommentTypeId
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateConsultantCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
