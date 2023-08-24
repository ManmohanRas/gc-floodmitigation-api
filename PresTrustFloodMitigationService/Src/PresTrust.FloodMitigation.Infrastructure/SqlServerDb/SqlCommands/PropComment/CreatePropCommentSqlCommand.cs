namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class CreatePropCommentSqlCommand
    {
        private readonly string _sqlCommand =
            @"INSERT INTO [Flood].[ParcelComment]
              (
                 Comment
                ,CommentTypeId
                ,ApplicationId
                ,Pamspin
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_Comment
               ,@p_CommentTypeId
               ,@p_ApplicationId
               ,@P_Pamspin
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

        public CreatePropCommentSqlCommand()
        {
        }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
