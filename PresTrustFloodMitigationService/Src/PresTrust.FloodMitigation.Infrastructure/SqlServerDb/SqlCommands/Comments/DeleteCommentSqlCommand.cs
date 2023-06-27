namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

class DeleteCommentSqlCommand
{
    private readonly string _sqlCommand =
       @" DELETE 
              FROM [Flood].[FloodComment]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
