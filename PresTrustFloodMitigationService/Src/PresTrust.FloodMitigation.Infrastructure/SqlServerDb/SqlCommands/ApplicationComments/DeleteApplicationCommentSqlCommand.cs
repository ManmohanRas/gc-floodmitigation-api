namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

class DeleteApplicationCommentSqlCommand
{
    private readonly string _sqlCommand =
       @" DELETE 
              FROM [Flood].[FloodApplicationComment]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteApplicationCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
