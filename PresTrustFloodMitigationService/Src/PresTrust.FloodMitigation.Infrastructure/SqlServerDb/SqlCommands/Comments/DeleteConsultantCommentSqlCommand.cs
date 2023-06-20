namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands.Comments;

class DeleteConsultantCommentSqlCommand
{
    private readonly string _sqlCommand =
        @" DELETE 
              FROM [Hist].[HistConsultantComment]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteConsultantCommentSqlCommand()
    {
    }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
