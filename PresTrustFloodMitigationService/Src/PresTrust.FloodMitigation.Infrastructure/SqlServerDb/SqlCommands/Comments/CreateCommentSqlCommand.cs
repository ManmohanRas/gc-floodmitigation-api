namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

public class CreateCommentSqlCommand
{
    private readonly string _sqlCommand =
                    @"INSERT INTO [Flood].[FloodComment]
              (
                 Comment
                ,CommentTypeId
                ,ApplicationId
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_Comment
               ,@p_CommentTypeId
               ,@p_ApplicationId
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

    public CreateCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
