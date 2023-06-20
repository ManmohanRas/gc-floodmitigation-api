namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateConsultantCommentSqlCommand
{
    private readonly string _sqlCommand =
        @"INSERT INTO [Hist].[HistConsultantComment]
              (
                 Comment
                ,CommentTypeId
                ,ApplicationId
                ,MarkRead
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_Comment
               ,@p_CommentTypeId
               ,@p_ApplicationId
               ,@p_MarkRead
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateConsultantCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
