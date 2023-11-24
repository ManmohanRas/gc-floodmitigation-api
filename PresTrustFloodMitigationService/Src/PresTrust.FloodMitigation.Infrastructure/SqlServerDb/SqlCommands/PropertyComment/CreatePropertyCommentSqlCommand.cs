namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands
{
    public class CreatePropertyCommentSqlCommand
    {
        private readonly string _sqlCommand =
            @"INSERT INTO [Flood].[FloodParcelComment]
              (
                 Comment
                ,CommentTypeId
                ,ApplicationId
                ,PamsPin
                ,LastUpdatedBy
                ,LastUpdatedOn
              )
              VALUES(
                @p_Comment
               ,@p_CommentTypeId
               ,@p_ApplicationId
               ,@P_PamsPin
               ,@p_LastUpdatedBy
               ,GetDate());

			SELECT CAST( SCOPE_IDENTITY() AS INT);";

        public CreatePropertyCommentSqlCommand()
        {
        }

        public override string ToString()
        {
            return _sqlCommand;
        }
    }
}
