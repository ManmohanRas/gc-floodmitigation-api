namespace PresTrust.FloodMitigation.Infrastructure.SqlServerDb.SqlCommands;

internal class DeleteFeedbackSqlCommand
{
    private readonly string _sqlCommand =
       @"DELETE 
              FROM [Hist].[HistFeedback]
              WHERE Id = @p_Id AND ApplicationId = @p_ApplicationId;";

    public DeleteFeedbackSqlCommand() { }

    public override string ToString()
    {
        return _sqlCommand;
    }
}
