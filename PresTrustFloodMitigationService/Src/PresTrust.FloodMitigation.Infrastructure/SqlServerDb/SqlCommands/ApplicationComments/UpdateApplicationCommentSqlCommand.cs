namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

class UpdateApplicationCommentSqlCommand
{
    private readonly string _sqlCommand =
       @"  UPDATE [Flood].[FloodApplicationComment]
                SET Comment = @p_Comment
                    ,CommentTypeId = @p_CommentTypeId
                   ,LastUpdatedBy = @p_LastUpdatedBy
                   ,LastUpdatedOn = GETDATE()
                WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public UpdateApplicationCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
